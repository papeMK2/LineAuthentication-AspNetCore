using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace LineAccountExtension
{
    public class LineAccountOptions : OAuthOptions
    {
        public LineAccountOptions()
        {
            CallbackPath = new PathString("/signin-line");
            AuthorizationEndpoint = LineAuthenticationConstants.AuthorizationEndpoint;
            TokenEndpoint = LineAuthenticationConstants.TokenEndpoint;
            UserInformationEndpoint = LineAuthenticationConstants.UserInformationEndpoint;
            Scope.Add("openid");
            Scope.Add("profile");
            Scope.Add("email");

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "userId");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "displayName");
            ClaimActions.MapJsonKey("urn:line:picture", "pictureUrl");
            ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
        }

        public string AppId
        {
            get => ClientId;
            set => ClientId = value;
        }
        public string AppSecret
        {
            get => ClientSecret;
            set => ClientSecret = value;
        }
    }
}
