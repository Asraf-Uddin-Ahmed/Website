using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Container;
namespace Website.Foundation.Core.Services
{
    public interface IUserService
    {
        bool DeleteUser(Guid userID);
        ICollection<User> GetAllUserPaged(int pageNumber, int pageSize, SortBy<User> sortBy);
        User GetUser(Guid userID);
        User GetUserByEmail(string email);
        User GetUserByUserName(string userName);
        bool IsEmailAddressAlreadyInUse(string email);
        bool IsUserNameAlreadyInUse(string userName);
        bool UpdateUserInformation(User user);
        ICollection<User> GetAll();
    }
}
