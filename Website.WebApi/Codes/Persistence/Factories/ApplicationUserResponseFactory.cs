using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Identity;
using Website.WebApi.Codes.Core.Factories;
using Website.WebApi.Models.Response;

namespace Website.WebApi.Codes.Persistence.Factories
{
    public class ApplicationUserResponseFactory : ResponseFactory, IApplicationUserResponseFactory
    {
        private ApplicationUserManager _applicationUserManager;
        public ApplicationUserResponseFactory(HttpRequestMessage httpRequestMessage, 
            ApplicationUserManager applicationUserManager)
            :base(httpRequestMessage)
        {
            _applicationUserManager = applicationUserManager;
        }

        public ApplicationUserResponseModel Create(ApplicationUser applicationUser)
        {
            return new ApplicationUserResponseModel()
            {
                Url = base.UrlHelper.Link("GetUserById", new { id = applicationUser.Id }),
                ID = applicationUser.Id,
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                EmailConfirmed = applicationUser.EmailConfirmed,
                Roles = _applicationUserManager.GetRolesAsync(applicationUser.Id).Result,
                Claims = _applicationUserManager.GetClaimsAsync(applicationUser.Id).Result
            };
        }

        public IEnumerable<ApplicationUserResponseModel> Create(IEnumerable<ApplicationUser> applicationUsers)
        {
            return applicationUsers.Select(u => this.Create(u));
        }
    }
}