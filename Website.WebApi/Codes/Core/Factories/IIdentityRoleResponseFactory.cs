using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.WebApi.Models.Response;

namespace Website.WebApi.Codes.Core.Factories
{
    public interface IIdentityRoleResponseFactory
    {
        IdentityRoleResponseModel Create(IdentityRole appRole);
        IEnumerable<IdentityRoleResponseModel> Create(IEnumerable<IdentityRole> identityRoles);
    }
}
