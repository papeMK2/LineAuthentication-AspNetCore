using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LineAccountExtension
{
    public static class LineAccountExtensions
    {
        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder)
        {
            return builder.AddLineAccount(LineAccountDefault.AuthenticationScheme, _ => { });
        }

        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder, 
            Action<LineAccountOptions> configureOptions)
        {
            return builder.AddLineAccount(LineAccountDefault.AuthenticationScheme, configureOptions);
        }

        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder,
            string authenticationScheme,
            Action<LineAccountOptions> configureOptions)
        {
            return builder.AddLineAccount(authenticationScheme, LineAccountDefault.DisplayName, configureOptions);
        }

        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder,
            string authenticationScheme,
            string displayName,
            Action<LineAccountOptions> configureOptions)
        {
            return builder.AddOAuth<LineAccountOptions, LineAccountHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
