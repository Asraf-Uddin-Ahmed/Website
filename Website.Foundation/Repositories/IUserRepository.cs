using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Aggregates;
using Website.Foundation.Container;

namespace Website.Foundation.Repositories
{
    public interface IUserRepository : IBaseEfRepository<User>
    {
        bool IsUserNameExist(string userName);
        bool IsEmailExist(string email);
        string ResetPassword(IUser user);
        string GetPassword(IUser user); 
        int GetTotalAnd(UserSearch searchItem);
        int GetTotalOr(UserSearch searchItem);
        IEnumerable<IUser> GetPagedAnd(UserSearch searchItem, int pageNumber, int pageSize, Func<IUser, dynamic> orderBy);
        IEnumerable<IUser> GetPagedOr(UserSearch searchItem, int pageNumber, int pageSize, Func<IUser, dynamic> orderBy);
    }
}
