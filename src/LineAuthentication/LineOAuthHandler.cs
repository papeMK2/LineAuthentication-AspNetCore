using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
#if NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json.Linq;
#else
using System.Text.Json;
#endif



namespace LineAuthentication
{
    /// <summary>
    /// Authentication handler for LINE's OAuth based authentication.
    /// </summary>
    public class LineOAuthHandler : OAuthHandler<LineOAuthOptions>
    {
        /// <inheritdoc />
        public LineOAuthHandler(IOptionsMonitor<LineOAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }


        /// <inheritdoc />
        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var parameters = new KeyValuePair<string, string>[]
            {
#if NETSTANDARD2_0_OR_GREATER
                new("id_token", tokens.Response.Value<string>("id_token") ?? string.Empty),
#else
                new("id_token", tokens.Response.RootElement.GetString("id_token") ?? string.Empty),
#endif
                new("client_id", this.Options.ClientId),
            };
            using var request = new HttpRequestMessage(HttpMethod.Post, this.Options.UserInformationEndpoint);
            using var httpContent = new FormUrlEncodedContent(parameters!);
            request.Content = httpContent;
            request.Headers.Accept.Add(new("application/json"));
            using var response = await this.Backchannel.SendAsync(request, this.Context.RequestAborted).ConfigureAwait(false);

#if NET5_0_OR_GREATER
            var payload = await response.Content.ReadAsStringAsync(this.Context.RequestAborted).ConfigureAwait(false);
#else
            var payload = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif

            if (!response.IsSuccessStatusCode)
            {
                var headers = response.Headers.ToString();
                this.Logger.LogError("An error occurred while verifying the ID token. The remote server returned a {Status} response with the following payload: {Headers} {Body}.", response.StatusCode, headers, payload);
#if NET5_0_OR_GREATER
                throw new HttpRequestException("An error occurred while verifying the ID token.", null, response.StatusCode);
#else
                throw new HttpRequestException("An error occurred while verifying the ID token.", null);
#endif
            }

            var principal = new ClaimsPrincipal(identity);

#if NETSTANDARD2_0_OR_GREATER
            var user = JObject.Parse(payload);
            var context = new OAuthCreatingTicketContext(principal, properties, this.Context, this.Scheme, this.Options, this.Backchannel, tokens, user);
#else
            using var json = JsonDocument.Parse(payload);
            var context = new OAuthCreatingTicketContext(principal, properties, Context, Scheme, Options, Backchannel, tokens, json.RootElement);
#endif

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
            };
            return QueryHelpers.AddQueryString(this.Options.AuthorizationEndpoint, queryString!);
        }
    }
}
