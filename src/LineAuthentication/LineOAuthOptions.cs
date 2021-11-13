using System.Security.Claims;
using LineAuthentication.Entities.MessagingApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace LineAuthentication;



/// <summary>
/// Configuration options for <see cref="LineOAuthHandler"/>.
/// </summary>
public class LineOAuthOptions : OAuthOptions
{
    #region Properties
    /// <summary>
    /// Used to force the consent screen to be displayed even if the user has already granted all requested permissions.<br/>
    /// <br/>
    /// When set to <c>true</c>, Line displays the consent screen for every authorization request.<br/>
    /// When left to <c>false</c>, the consent screen is skipped if the user has already granted.
    /// </summary>
    public bool Prompt { get; set; }


    /// <summary>
    /// Display the option to add the official LINE account when the user login.<br/>
    /// <br/>
    /// When set to <c>None</c>, doesn't display the add friend option on the consent screen.<br/>
    /// When set to <c>Normal</c>, display the add friend option on the consent screen.<br/>
    /// When set to <c>Aggressive</c>, display the add friend option after the consent screen.
    /// </summary>
    public BotPrompt BotPrompt { get; set; }


    /// <summary>
    /// Used to set the QR code login as the default login method.<br/>
    /// <br/>
    /// When set to <c>true</c>, fefault login method is set to QR Code.<br/>
    /// When set to <c>false</c>, fefault login method is set to Email and Password.
    /// </summary>
    public bool UseQRLogin { get; set; }


    /// <summary>
    /// Used to enable change login method on the consent screen.<br/>
    /// <br/>
    /// When set to <c>true</c>, enable change login method. Default value is <c>true</c>.<br/>
    /// When set to <c>false</c>, disable change login method.
    /// </summary>
    public bool EnableSwitchLoginMethod { get; set; } = true;


    /// <summary>
    /// Used to disable auto login.<br/>
    /// <br/>
    /// When set to <c>true</c>, disable auto login.<br/>
    /// When set to <c>false</c>, enable auto login.
    /// </summary>
    public bool DisableAutoLogin { get; set; }
    #endregion


    #region Constructors
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
    #endregion
}
