namespace LineAuthentication.Internals
{
    /// <summary>
    /// Default values for LINE authentication
    /// </summary>
    internal static class LineDefaults
    {
        /// <summary>
        /// The default scheme for LINE authentication. Defaults to <c>Line</c>.
        /// </summary>
        public const string AuthenticationScheme = "Line";


        /// <summary>
        /// The default display name for LINE authentication. Defaults to <c>LINE</c>.
        /// </summary>
        public const string DisplayName = "LINE";


        /// <summary>
        /// The default endpoint used to perform LINE authentication.
        /// </summary>
        public const string AuthorizationEndpoint = "https://access.line.me/oauth2/v2.1/authorize";


        /// <summary>
        /// The OAuth endpoint used to exchange access tokens.
        /// </summary>
        public const string TokenEndpoint = "https://api.line.me/oauth2/v2.1/token";


        /// <summary>
        /// The LINE endpoint that is used to gather additional user information.
        /// </summary>
        public const string UserInformationEndpoint = "https://api.line.me/v2/profile";
    }
}
