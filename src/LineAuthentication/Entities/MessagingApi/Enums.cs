using System;

namespace LineAuthentication.Entities.MessagingApi;



/// <summary>
/// Option to add an official LINE account 
/// </summary>
public enum BotPromptMode
{
    /// <summary>
    /// Display the Add Friend option on the consent screen.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Display the Add Friends option after the consent screen
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
            BotPromptMode.Normal => "normal",
            BotPromptMode.Aggressive => "aggressive",
            _ => throw new ArgumentOutOfRangeException(nameof(mode)),
        };
}
