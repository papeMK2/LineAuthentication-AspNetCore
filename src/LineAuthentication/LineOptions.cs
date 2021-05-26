using System.Security.Claims;
using LineAuthentication.Internals;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;



namespace LineAuthentication
{
    /// <summary>
    /// Configuration options for <see cref="LineHandler"/>.
    /// </summary>
    public sealed class LineOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new <see cref="LineOptions"/>.
        /// </summary>
        public LineOptions()
        {
            this.CallbackPath = new("/signin-line");
            this.AuthorizationEndpoint = LineDefaults.AuthorizationEndpoint;
            this.TokenEndpoint = LineDefaults.TokenEndpoint;
            this.UserInformationEndpoint = LineDefaults.UserInformationEndpoint;
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
