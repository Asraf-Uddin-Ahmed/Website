using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Factories;
using Website.Foundation.Core.Aggregates.Identity;

namespace Website.Foundation.Persistence.Factories
{
    public class ApplicationUserFactory : IApplicationUserFactory
    {
        public ApplicationUser Create(string userName, string email)
        {
            ApplicationUser appUser = new ApplicationUser()
            {
                UserName = userName,
                Email = email,
                Id = GuidUtility.GetNewSequentialGuid(),
            };
            return appUser;
        }
    }
}
