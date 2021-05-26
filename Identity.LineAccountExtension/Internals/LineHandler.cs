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



namespace LineAccountExtension.Internals
{
    /// <summary>
    /// Authentication handler for LINE's OAuth based authentication.
    /// </summary>
    internal sealed class LineHandler : OAuthHandler<LineOptions>
    {
        /// <inheritdoc />
        public LineHandler(IOptionsMonitor<LineOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }


        /// <inheritdoc />
        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this.Options.UserInformationEndpoint);
            request.Headers.Authorization = new("Bearer", tokens.AccessToken);
            var response = await this.Backchannel.SendAsync(request, this.Context.RequestAborted).ConfigureAwait(false);

#if NET5_0_OR_GREATER
            var payload = await response.Content.ReadAsStringAsync(this.Context.RequestAborted).ConfigureAwait(false);
#else
            var payload = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif

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
            var state = this.Options.StateDataFormat.Protect(properties);
            var queryString = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["response_type"] = "code",
                ["client_id"] = this.Options.ClientId,
                ["redirect_uri"] = redirectUri,
                ["scope"] = string.Join(" ", this.Options.Scope),
                ["state"] = state,
            };
            return QueryHelpers.AddQueryString(this.Options.AuthorizationEndpoint, queryString!);
        }


        /// <inheritdoc />
        protected override string FormatScope(IEnumerable<string> scopes)
            => string.Join("%20", scopes);
    }
}
