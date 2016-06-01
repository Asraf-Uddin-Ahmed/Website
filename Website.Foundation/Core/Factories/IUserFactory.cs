using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Enums;

namespace Website.Foundation.Core.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string userName, string email, string password, string name, UserType type, UserStatus status);
    }
}
