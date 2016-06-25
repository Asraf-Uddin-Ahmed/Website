using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Website.Identity.Aggregates;
using Website.Identity.Managers;
namespace Website.Identity.Helpers
{
    public interface IAuthHelper
    {
        Task<ClaimsIdentity> GetClaimIdentityAsync(ApplicationUser appUser, ApplicationUserManager appUserManager);
    }
}
