using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Identity;
using Website.WebApi.Models.Response;

namespace Website.WebApi.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private ApplicationUserManager _AppUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
        }


        public IdentityRoleResponseModel Create(IdentityRole appRole)
        {
            return new IdentityRoleResponseModel
            {
                Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
                ID = appRole.Id,
                Name = appRole.Name
            };
        }
    }

}