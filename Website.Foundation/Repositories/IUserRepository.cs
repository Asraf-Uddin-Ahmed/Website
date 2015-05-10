using System;
using Website.Foundation.Aggregates;
using Website.Foundation.Container;
namespace Website.Foundation.Repositories
{
    public interface IUserRepository : IBaseEfRepository<User>
    {
        System.Collections.Generic.IEnumerable<Website.Foundation.Aggregates.IUser> GetPagedAnd(Website.Foundation.Container.UserSearch searchItem, int pageNumber, int pageSize, SortBy<User> sortBy);
        System.Collections.Generic.IEnumerable<Website.Foundation.Aggregates.IUser> GetPagedOr(Website.Foundation.Container.UserSearch searchItem, int pageNumber, int pageSize, SortBy<User> sortBy);
        int GetTotalAnd(Website.Foundation.Container.UserSearch searchItem);
        int GetTotalOr(Website.Foundation.Container.UserSearch searchItem);
        bool IsEmailExist(string email);
        bool IsUserNameExist(string userName);
        IUser GetByUserName(string userName);
        IUser GetByEmail(string email);
    }
}
