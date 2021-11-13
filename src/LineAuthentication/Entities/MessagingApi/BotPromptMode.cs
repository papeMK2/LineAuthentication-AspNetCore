using System;

namespace LineAuthentication.Entities.MessagingApi;



/// <summary>
/// Option to add an official LINE account 
/// </summary>
public enum BotPromptMode
{
    /// <summary>
    /// Doesn't display the add friend option on the consent screen.
    /// </summary>
    None = 0,

    /// <summary>
    /// Display the add friend option on the consent screen.
    /// </summary>
    Normal,

    /// <summary>
    /// Display the add friend option after the consent screen
    /// </summary>
    Aggressive,
}



/// <summary>
/// Provides <see cref="BotPromptMode"/> extension methods.
/// </summary>
internal static class BotPromptModeExtensions
{
    /// <summary>
    /// Convert to 'bot_prompt' option value string.
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string ToOptionString(this BotPromptMode mode)
        => mode switch
        {
            BotPromptMode.None => string.Empty,
            BotPromptMode.Normal => "normal",
            BotPromptMode.Aggressive => "aggressive",
            _ => throw new ArgumentOutOfRangeException(nameof(mode)),
        };
}
