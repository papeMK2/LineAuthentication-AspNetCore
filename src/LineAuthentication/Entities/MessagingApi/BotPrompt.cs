using System;

namespace LineAuthentication.Entities.MessagingApi;



/// <summary>
/// Option to add an official LINE account 
/// </summary>
public enum BotPrompt
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
/// Provides <see cref="BotPrompt"/> extension methods.
/// </summary>
internal static class BotPromptExtensions
{
    /// <summary>
    /// Convert to 'bot_prompt' option value string.
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string ToOptionString(this BotPrompt prompt)
        => prompt switch
        {
            BotPrompt.None => string.Empty,
            BotPrompt.Normal => "normal",
            BotPrompt.Aggressive => "aggressive",
            _ => throw new ArgumentOutOfRangeException(nameof(prompt)),
        };
}
