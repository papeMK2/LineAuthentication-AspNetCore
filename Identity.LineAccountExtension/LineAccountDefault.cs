using System;
using System.Collections.Generic;
using System.Text;

namespace LineAccountExtension
{
    public static class LineAccountDefault
    {
        public const string AuthenticationScheme = "Line";
        public static readonly string DisplayName = "Line";
        public static readonly string AuthorizationEndpoint = "https://access.line.me/oauth2/v2.1/authorize";
        public static readonly string TokenEndpoint = "https://api.line.me/oauth2/v2.1/token";
        public static readonly string UserInformationEndpoint = "https://api.line.me/v2/profile";
    }
}
