using System;
using System.Collections.Generic;
using System.Text;
using FastEnumUtility;

namespace LineAuthentication.Entities.MessagingApi;

/// <summary>
/// Option to add an official LINE account 
/// </summary>
public enum BotPromptMode
{
    /// <summary>
    /// Display the Add Friend option on the consent screen.
    /// </summary>
    [Label("normal")]
    Normal,
    /// <summary>
    /// Display the Add Friends option after the consent screen
    /// </summary>
    [Label("aggressive")]
    Aggressive
}
