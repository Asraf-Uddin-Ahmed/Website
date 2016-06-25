using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Website.Identity.Aggregates;
using Website.Identity.Managers;
using Website.Identity.Providers;

namespace Website.Identity.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        public async Task<ClaimsIdentity> GetClaimIdentityAsync(ApplicationUser appUser, ApplicationUserManager appUserManager)
        {
            ClaimsIdentity oAuthIdentity = await appUser.GenerateUserIdentityAsync(appUserManager, "JWT");
            oAuthIdentity.AddClaims(ExtendedClaimsProvider.GetClaims(appUser));
            oAuthIdentity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(oAuthIdentity));
            return oAuthIdentity;
        }
    }
}
