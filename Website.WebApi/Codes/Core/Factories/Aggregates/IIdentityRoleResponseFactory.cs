using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.WebApi.Models.Response;
using Website.Foundation.Core.Aggregates.Identity;
using Website.WebApi.Models.Response.Aggregates;

namespace Website.WebApi.Codes.Core.Factories.Aggregates
{
    public interface IIdentityRoleResponseFactory
    {
        IdentityRoleResponseModel Create(CustomRole appRole);
        ICollection<IdentityRoleResponseModel> Create(ICollection<CustomRole> identityRoles);
    }
}
