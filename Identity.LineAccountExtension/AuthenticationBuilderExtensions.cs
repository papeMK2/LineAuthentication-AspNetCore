using System;
using LineAccountExtension.Internals;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace LineAccountExtension
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder)
            => AddLine(builder, static _ => { });


        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, Action<LineOptions> configureOptions)
            => AddLine(builder, LineAuthenticationConstants.AuthenticationScheme, configureOptions);


        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, string authenticationScheme, Action<LineOptions> configureOptions)
            => AddLine(builder, authenticationScheme, LineAuthenticationConstants.DisplayName, configureOptions);


        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<LineOptions> configureOptions)
            => builder.AddOAuth<LineOptions, LineAccountHandler>(authenticationScheme, displayName, configureOptions);
    }
}
