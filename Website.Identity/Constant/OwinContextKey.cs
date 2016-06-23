using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Identity.Constant
{
    internal static class OwinContextKey
    {
        internal const string CLIENT_ALLOWED_ORIGIN = "as:clientAllowedOrigin";
        internal const string CLIENT_REFRESH_TOKEN_LIFE_TIME = "as:clientRefreshTokenLifeTime";
    }
}
