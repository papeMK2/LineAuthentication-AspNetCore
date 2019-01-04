using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
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
        public LineAccountHandler(IOptionsMonitor<LineAccountOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload);
            context.RunClaimActions();
            
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var queryString = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            queryString.Add("response_type", "code");
            queryString.Add("client_id", Options.ClientId);
            queryString.Add("redirect_uri", redirectUri);

            queryString.Add("scope", string.Join(" ", Options.Scope));


            var state = Options.StateDataFormat.Protect(properties);
            queryString.Add("state", state);

            var authorizationEndpoint = QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryString);
            return authorizationEndpoint;
        }

        protected override string FormatScope(IEnumerable<string> scopes)
        {
            return string.Join("%20", scopes);
        }

        protected override string FormatScope()
        {
            return base.FormatScope();
        }
    }
}
