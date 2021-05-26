using System;
using LineAccountExtension.Internals;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace LineAccountExtension
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder)
            => AddLineAccount(builder, static _ => { });


        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder, Action<LineOptions> configureOptions)
            => AddLineAccount(builder, LineAuthenticationConstants.AuthenticationScheme, configureOptions);


        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder, string authenticationScheme, Action<LineOptions> configureOptions)
            => AddLineAccount(builder, authenticationScheme, LineAuthenticationConstants.DisplayName, configureOptions);


        public static AuthenticationBuilder AddLineAccount(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<LineOptions> configureOptions)
            => builder.AddOAuth<LineOptions, LineAccountHandler>(authenticationScheme, displayName, configureOptions);
    }
}
