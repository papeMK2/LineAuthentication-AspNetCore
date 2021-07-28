using System;
using LineAuthentication.Internals;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;



namespace LineAuthentication
{
    /// <summary>
    /// Extension methods to configure LINE OAuth authentication.
    /// </summary>
    public static class LineOAuthExtensions
    {
        /// <summary>
        /// Adds LINE OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
        /// The default scheme is specified by <see cref="LineOAuthDefaults.AuthenticationScheme"/>.
        /// <para>
        /// LINE authentication allows application users to sign in with their LINE account.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder)
            => AddLine(builder, static _ => { });


        /// <summary>
        /// Adds LINE OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
        /// The default scheme is specified by <see cref="LineOAuthDefaults.AuthenticationScheme"/>.
        /// <para>
        /// LINE authentication allows application users to sign in with their LINE account.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <param name="configureOptions">A delegate to configure <see cref="LineOAuthOptions"/>.</param>
        /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, Action<LineOAuthOptions> configureOptions)
            => AddLine(builder, LineOAuthDefaults.AuthenticationScheme, configureOptions);


        /// <summary>
        /// Adds LINE OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
        /// The default scheme is specified by <see cref="LineOAuthDefaults.AuthenticationScheme"/>.
        /// <para>
        /// LINE authentication allows application users to sign in with their LINE account.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureOptions">A delegate to configure <see cref="LineOAuthOptions"/>.</param>
        /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, string authenticationScheme, Action<LineOAuthOptions> configureOptions)
            => AddLine(builder, authenticationScheme, LineOAuthDefaults.DisplayName, configureOptions);


        /// <summary>
        /// Adds LINE OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
        /// The default scheme is specified by <see cref="LineOAuthDefaults.AuthenticationScheme"/>.
        /// <para>
        /// LINE authentication allows application users to sign in with their LINE account.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="displayName">A display name for the authentication handler.</param>
        /// <param name="configureOptions">A delegate to configure <see cref="LineOAuthOptions"/>.</param>
        /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<LineOAuthOptions> configureOptions)
            => builder.AddOAuth<LineOAuthOptions, LineOAuthHandler>(authenticationScheme, displayName, configureOptions);
    }
}
