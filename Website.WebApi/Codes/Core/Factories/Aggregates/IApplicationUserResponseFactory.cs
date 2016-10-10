using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Aggregates.Identity;
using Website.WebApi.Models.Response;
using Website.WebApi.Models.Response.Aggregates;

namespace Website.WebApi.Codes.Core.Factories.Aggregates
{
    public interface IApplicationUserResponseFactory
    {
        ApplicationUserResponseModel Create(ApplicationUser applicationUser);
        ICollection<ApplicationUserResponseModel> Create(ICollection<ApplicationUser> applicationUsers);
    }
}
