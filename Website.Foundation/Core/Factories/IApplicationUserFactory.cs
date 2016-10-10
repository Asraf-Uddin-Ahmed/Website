using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Aggregates.Identity;

namespace Website.Foundation.Core.Factories
{
    public interface IApplicationUserFactory
    {
        ApplicationUser Create(string userName, string email);
    }
}
