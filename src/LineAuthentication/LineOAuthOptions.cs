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
    /// Used to force the consent screen to be displayed even if the user has already granted all requested permissions.
    /// When set to <c>true</c>, Line displays the consent screen for every authorization request.
    /// When left to <c>false</c>, the consent screen is skipped if the user has already granted.
    /// </summary>
    public bool Prompt { get; set; }


    /// <summary>
    /// Display the option to add the official LINE account when the user login
    /// 
    /// When set to <c>true</c>, Display the Add Friend option on the consent screen.
    /// Use the mode set to BotPromptMode.The default value is normal.
    /// When set to <c>false</c>, Don't display the Add Friend option on the  consent screen.
    /// </summary>
    public bool BotPrompt { get; set; }


    /// <summary>
    /// Option to add an official LINE account
    /// 
    /// When set to <c>Normal</c>, display the Add Friend option on the consent screen
    /// When set to <c>Aggressive</c>, display the Add Friends option after the consent screen
    /// </summary>
    public BotPromptModel BotPromptMode { get; set; }


    /// <summary>
    /// Used to set the QR code login as the default login method.
    /// When set to <c>true</c>, Default login method is set to QR Code
    /// When set to <c>false</c>, Default login method is set to Email and Password.
    /// </summary>
    public bool UseQRLogin { get; set; }


    /// <summary>
    /// Used to enable change login method on the consent screen.
    /// When set to <c>true</c>, Enable change login method.
    /// Default value is true.
    /// When set to <c>false</c>, Disable change login method.
    /// </summary>
    public bool EnableSwitchLoginMethod { get; set; } = true;


    /// <summary>
    /// Used to disable auto login.
    /// When set to <c>true</c>, Disable auto login.
    /// When set to <c>false</c>, Enable auto login.
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
