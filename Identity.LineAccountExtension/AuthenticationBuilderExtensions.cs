using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace LineAccountExtension
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder)
            => AddLineAccount(builder, static _ => { });


        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder, Action<LineAccountOptions> configureOptions)
            => AddLineAccount(builder, LineAccountDefault.AuthenticationScheme, configureOptions);


        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder, string authenticationScheme, Action<LineAccountOptions> configureOptions)
            => AddLineAccount(builder, authenticationScheme, LineAccountDefault.DisplayName, configureOptions);


        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<LineAccountOptions> configureOptions)
            => builder.AddOAuth<LineAccountOptions, LineAccountHandler>(authenticationScheme, displayName, configureOptions);
    }
}
