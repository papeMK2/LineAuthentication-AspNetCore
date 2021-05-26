using System.Security.Claims;
using LineAccountExtension.Internals;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace LineAccountExtension
{
    public class LineOptions : OAuthOptions
    {
        public LineOptions()
        {
            this.CallbackPath = new("/signin-line");
            this.AuthorizationEndpoint = LineAuthenticationConstants.AuthorizationEndpoint;
            this.TokenEndpoint = LineAuthenticationConstants.TokenEndpoint;
            this.UserInformationEndpoint = LineAuthenticationConstants.UserInformationEndpoint;
            this.Scope.Add("openid");
            this.Scope.Add("profile");
            this.Scope.Add("email");
            this.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "userId");
            this.ClaimActions.MapJsonKey(ClaimTypes.Name, "displayName");
            this.ClaimActions.MapJsonKey("urn:line:picture", "pictureUrl");
            this.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
        }

        public string AppId
        {
            get => this.ClientId;
            set => this.ClientId = value;
        }

        public string AppSecret
        {
            get => this.ClientSecret;
            set => this.ClientSecret = value;
        }
    }
}
