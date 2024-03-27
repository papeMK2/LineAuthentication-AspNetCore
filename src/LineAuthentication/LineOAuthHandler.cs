using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using LineAuthentication.MessagingApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LineAuthentication;



/// <summary>
/// Authentication handler for LINE's OAuth based authentication.
/// </summary>
public class LineOAuthHandler : OAuthHandler<LineOAuthOptions>
{
    /// <inheritdoc />
#pragma warning disable CS0618
    public LineOAuthHandler(IOptionsMonitor<LineOAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }
#pragma warning restore CS0618


    /// <inheritdoc />
    protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
    {
        var parameters = new KeyValuePair<string, string>[]
        {
            new("id_token", tokens.Response?.RootElement.GetString("id_token") ?? string.Empty),
            new("client_id", this.Options.ClientId),
        };
        using var request = new HttpRequestMessage(HttpMethod.Post, this.Options.UserInformationEndpoint);
        using var httpContent = new FormUrlEncodedContent(parameters!);
        request.Content = httpContent;
        request.Headers.Accept.Add(new("application/json"));
        using var response = await this.Backchannel.SendAsync(request, this.Context.RequestAborted).ConfigureAwait(false);
        var payload = await response.Content.ReadAsStringAsync(this.Context.RequestAborted).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var headers = response.Headers.ToString();
            this.Logger.LogError("An error occurred while verifying the ID token. The remote server returned a {Status} response with the following payload: {Headers} {Body}.", response.StatusCode, headers, payload);
            throw new HttpRequestException("An error occurred while verifying the ID token.", null, response.StatusCode);
        }

        var principal = new ClaimsPrincipal(identity);
        using var json = JsonDocument.Parse(payload);
        var context = new OAuthCreatingTicketContext(principal, properties, Context, Scheme, Options, Backchannel, tokens, json.RootElement);
        context.RunClaimActions();
        await this.Events.CreatingTicket(context).ConfigureAwait(false);
        return new(context.Principal!, context.Properties, this.Scheme.Name);
    }


    /// <inheritdoc />
    protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
    {
        var queryString = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["response_type"] = "code",
            ["client_id"] = this.Options.ClientId,
            ["redirect_uri"] = redirectUri,
            ["scope"] = this.FormatScope(),
            ["state"] = this.Options.StateDataFormat.Protect(properties),
            ["prompt"] = this.Options.Prompt ? "consent" : string.Empty,
            ["bot_prompt"] = this.Options.BotPrompt.ToOptionString(),
            ["initial_amr_display"] = this.Options.UseQRLogin ? "lineqr" : "",
            ["disable_auto_login"] = this.Options.EnableAutoLogin ? "false" : "true",
            ["switch_amr"] = this.Options.EnableSwitchLoginMethod ? "true" : "false",
        };
        return QueryHelpers.AddQueryString(this.Options.AuthorizationEndpoint, queryString!);
    }
}
