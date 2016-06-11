using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Website.WebApi.Codes.Core.Factories;
using Website.WebApi.Models.Response;

namespace Website.WebApi.Codes.Persistence.Factories
{
    public class IdentityRoleResponseFactory : ResponseFactory, IIdentityRoleResponseFactory
    {
        public IdentityRoleResponseFactory(HttpRequestMessage httpRequestMessage)
            :base(httpRequestMessage)
        {
        }

        public IdentityRoleResponseModel Create(IdentityRole identityRole)
        {
            return new IdentityRoleResponseModel
            {
                Url = base.UrlHelper.Link("GetRoleById", new { id = identityRole.Id }),
                ID = identityRole.Id,
                Name = identityRole.Name
            };
        }

        public IEnumerable<IdentityRoleResponseModel> Create(IEnumerable<IdentityRole> identityRoles)
        {
            return identityRoles.Select(r => this.Create(r));
        }
    }
}