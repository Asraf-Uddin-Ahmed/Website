using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates.Identity;
using Website.Identity.Managers;
namespace Website.Identity.Helpers
{
    public interface IAuthHelper
    {
        Task<ClaimsIdentity> GetClaimIdentityAsync(ApplicationUser appUser, ApplicationUserManager appUserManager);
        AuthenticationProperties GetAuthenticationProperties(string userName, string clientID);
    }
}
