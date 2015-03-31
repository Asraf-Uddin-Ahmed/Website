using System;
using System.Collections.Generic;
using Website.Foundation.Aggregates;
namespace Website.Foundation.Services
{
    public interface IUserService
    {
        bool DeleteUser(Guid userID);
        System.Collections.Generic.ICollection<Website.Foundation.Aggregates.IUser> GetAllUserPaged(int pageNumber, int pageSize, Func<Website.Foundation.Aggregates.IUser, dynamic> orderBy);
        Website.Foundation.Aggregates.IUser GetUser(Guid userID);
        Website.Foundation.Aggregates.IUser GetUserByEmail(string email);
        Website.Foundation.Aggregates.IUser GetUserByUserName(string userName);
        bool IsEmailAddressAlreadyInUse(string email);
        bool IsUserNameAlreadyInUse(string userName);
        bool UpdateUserInformation(Website.Foundation.Aggregates.IUser user);
        ICollection<IUser> GetAll();
    }
}
