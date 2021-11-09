using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace LineAuthentication;



/// <summary>
/// Configuration options for <see cref="LineOAuthHandler"/>.
/// </summary>
public class LineOAuthOptions : OAuthOptions
{
    /// <summary>
    /// Used to force the consent screen to be displayed even if the user has already granted all requested permissions.
    /// When set to <c>true</c>, Line displays the consent screen for every authorization request.
    /// When left to <c>false</c>, the consent screen is skipped if the user has already granted.
    /// </summary>
    public bool Prompt { get; set; }


    /// <summary>
    /// Initializes a new <see cref="LineOAuthOptions"/>.
    /// </summary>
    public LineOAuthOptions()
    {
        this.CallbackPath = new("/signin-line");
        this.AuthorizationEndpoint = LineOAuthDefaults.AuthorizationEndpoint;
        this.TokenEndpoint = LineOAuthDefaults.TokenEndpoint;
        this.UserInformationEndpoint = LineOAuthDefaults.UserInformationEndpoint;
        this.Scope.Add("openid");
        this.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
    }
}
