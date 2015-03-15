using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Website.Foundation.Container;

namespace Website.Foundation.Repositories
{
    public class UserRepository : BaseEfRepository<User>, IUserRepository
    {
        private TableContext _context;
        [Inject]
        public UserRepository(TableContext context)
            : base(context)
        {
            _context = context;
        }

        public bool IsUserNameExist(string userName)
        {
            bool isExist = _context.Users.Any(col => col.UserName == userName);
            return isExist;
        }
        public bool IsEmailExist(string email)
        {
            bool isExist = _context.Users.Any(col => col.EmailAddress == email);
            return isExist;
        }
        public int GetTotalAnd(UserSearch searchItem)
        {
            int total = _context.Users.Count(col =>
                (searchItem.EmailAddress == null || searchItem.EmailAddress == col.EmailAddress)
                && (searchItem.UserName == null || searchItem.UserName == col.UserName)
                && (searchItem.TypeOfUser == null || searchItem.TypeOfUser == col.TypeOfUser)
                && (searchItem.Status == null || searchItem.Status == col.Status)
                && (searchItem.WrongPasswordAttempt == null || searchItem.WrongPasswordAttempt == col.WrongPasswordAttempt)
                && (searchItem.LastWrongPasswordAttempt == null || searchItem.LastWrongPasswordAttempt == col.LastWrongPasswordAttempt)
                && (searchItem.CreationTime == null || searchItem.CreationTime == col.CreationTime)
                && (searchItem.UpdateTime == null || searchItem.UpdateTime == col.UpdateTime));
            return total;
        }
        public int GetTotalOr(UserSearch searchItem)
        {
            bool isAllNull = searchItem.EmailAddress == null 
                && searchItem.UserName == null 
                && searchItem.TypeOfUser == null
                && searchItem.Status == null
                && searchItem.WrongPasswordAttempt == null
                && searchItem.LastWrongPasswordAttempt == null
                && searchItem.CreationTime == null
                && searchItem.UpdateTime == null;
            int total = _context.Users.Count(col =>
                (searchItem.EmailAddress != null && searchItem.EmailAddress == col.EmailAddress)
                || (searchItem.UserName != null && searchItem.UserName == col.UserName)
                || (searchItem.TypeOfUser != null && searchItem.TypeOfUser == col.TypeOfUser)
                || (searchItem.Status != null && searchItem.Status == col.Status)
                || (searchItem.WrongPasswordAttempt != null && searchItem.WrongPasswordAttempt == col.WrongPasswordAttempt)
                || (searchItem.LastWrongPasswordAttempt != null && searchItem.LastWrongPasswordAttempt == col.LastWrongPasswordAttempt)
                || (searchItem.CreationTime != null && searchItem.CreationTime == col.CreationTime)
                || (searchItem.UpdateTime != null && searchItem.UpdateTime == col.UpdateTime)
                || isAllNull);
            return total;
        }
    }
}
