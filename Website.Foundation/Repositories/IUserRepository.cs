using System;
using Website.Foundation.Aggregates;
namespace Website.Foundation.Repositories
{
    public interface IUserRepository : IBaseEfRepository<User>
    {
        System.Collections.Generic.IEnumerable<Website.Foundation.Aggregates.IUser> GetPagedAnd(Website.Foundation.Container.UserSearch searchItem, int pageNumber, int pageSize, Func<Website.Foundation.Aggregates.IUser, dynamic> predicateOrderBy);
        System.Collections.Generic.IEnumerable<Website.Foundation.Aggregates.IUser> GetPagedOr(Website.Foundation.Container.UserSearch searchItem, int pageNumber, int pageSize, Func<Website.Foundation.Aggregates.IUser, dynamic> predicateOrderBy);
        int GetTotalAnd(Website.Foundation.Container.UserSearch searchItem);
        int GetTotalOr(Website.Foundation.Container.UserSearch searchItem);
        bool IsEmailExist(string email);
        bool IsUserNameExist(string userName);
        IUser GetByUserName(string userName);
    }
}
