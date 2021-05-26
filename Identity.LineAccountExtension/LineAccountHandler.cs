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
using Newtonsoft.Json.Linq;

namespace LineAccountExtension
{
    public class LineAccountHandler : OAuthHandler<LineAccountOptions>
    {
        public LineAccountHandler(IOptionsMonitor<LineAccountOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this.Options.UserInformationEndpoint);
            request.Headers.Authorization = new("Bearer", tokens.AccessToken);

            var response = await this.Backchannel.SendAsync(request, this.Context.RequestAborted).ConfigureAwait(false);
            var payload = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var user = JObject.Parse(payload);
            var principal = new ClaimsPrincipal(identity);
            var context = new OAuthCreatingTicketContext(principal, properties, this.Context, this.Scheme, this.Options, this.Backchannel, tokens, user);
            context.RunClaimActions();
            return new(context.Principal, context.Properties, this.Scheme.Name);
        }

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
            return QueryHelpers.AddQueryString(this.Options.AuthorizationEndpoint, queryString);
        }

        protected override string FormatScope(IEnumerable<string> scopes)
        {
            return string.Join("%20", scopes);
        }
    }
}
