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


        /// <summary>
        /// Gets or sets the provider-assigned application id.
        /// </summary>
        /// <remarks>This property is an alias of <see cref="OAuthOptions.ClientId"/>.</remarks>
        public string AppId
        {
            get => this.ClientId;
            set => this.ClientId = value;
        }


        /// <summary>
        /// Gets or sets the provider-assigned application secret.
        /// </summary>
        /// <remarks>This property is an alias of <see cref="OAuthOptions.ClientSecret"/>.</remarks>
        public string AppSecret
        {
            get => this.ClientSecret;
            set => this.ClientSecret = value;
        }
    }
}
