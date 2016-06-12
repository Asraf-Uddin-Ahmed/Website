using System;
using System.Collections.Generic;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.SearchData;
namespace Website.Foundation.Core.Repositories
{
    public interface IUserRepository : IRepositorySearch<User, UserSearch>
    {
        bool IsEmailExist(string email);
        bool IsUserNameExist(string userName);
        User GetByUserName(string userName);
        User GetByEmail(string email);
    }
}
