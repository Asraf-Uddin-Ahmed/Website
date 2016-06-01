using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ratul.Utility;
using System.IO;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Container;
using System.Linq.Expressions;
using Website.Foundation.Core.Services;
using Website.Foundation.Core.SearchData;

namespace Website.Foundation.Persistence.Repositories
{
    public class UserRepository : RepositorySearch<User, UserSearch>, IUserRepository
    {
        private TableContext _context;
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
        public User GetByUserName(string userName)
        {
            User user = _context.Users.FirstOrDefault(col => col.UserName == userName);
            return user;
        }
        public User GetByEmail(string email)
        {
            User user = _context.Users.FirstOrDefault(col => col.EmailAddress == email);
            return user;
        }

        private Expression<Func<User, bool>> GetAndSearchCondition(UserSearch searchItem)
        {
            Expression<Func<User, bool>> predicate = (col) =>
                (searchItem.EmailAddress == null || searchItem.EmailAddress == col.EmailAddress)
                && (searchItem.UserName == null || searchItem.UserName == col.UserName)
                && (searchItem.TypeOfUser == null || searchItem.TypeOfUser == col.TypeOfUser)
                && (searchItem.Status == null || searchItem.Status == col.Status)
                && (searchItem.WrongPasswordAttempt == null || searchItem.WrongPasswordAttempt == col.WrongPasswordAttempt)
                && (searchItem.LastWrongPasswordAttempt == null || searchItem.LastWrongPasswordAttempt == col.LastWrongPasswordAttempt)
                && (searchItem.CreationTime == null || searchItem.CreationTime == col.CreationTime)
                && (searchItem.UpdateTime == null || searchItem.UpdateTime == col.UpdateTime);
            return predicate;
        }
        private Expression<Func<User, bool>> GetOrSearchCondition(UserSearch searchItem)
        {
            bool isAllNull = base.IsAllPropertyNull(searchItem);
            Expression<Func<User, bool>> predicate = (col) =>
                (searchItem.EmailAddress != null && searchItem.EmailAddress == col.EmailAddress)
                || (searchItem.UserName != null && searchItem.UserName == col.UserName)
                || (searchItem.TypeOfUser != null && searchItem.TypeOfUser == col.TypeOfUser)
                || (searchItem.Status != null && searchItem.Status == col.Status)
                || (searchItem.WrongPasswordAttempt != null && searchItem.WrongPasswordAttempt == col.WrongPasswordAttempt)
                || (searchItem.LastWrongPasswordAttempt != null && searchItem.LastWrongPasswordAttempt == col.LastWrongPasswordAttempt)
                || (searchItem.CreationTime != null && searchItem.CreationTime == col.CreationTime)
                || (searchItem.UpdateTime != null && searchItem.UpdateTime == col.UpdateTime)
                || isAllNull;
            return predicate;
        }
        public override int GetTotalAnd(UserSearch searchItem)
        {
            Expression<Func<User, bool>> predicateCount = GetAndSearchCondition(searchItem);
            int total = base.GetTotalBy(predicateCount);
            return total;
        }
        public override int GetTotalOr(UserSearch searchItem)
        {
            Expression<Func<User, bool>> predicateCount = GetOrSearchCondition(searchItem);
            int total = base.GetTotalBy(predicateCount);
            return total;
        }

        public override IEnumerable<User> GetByAnd(UserSearch searchItem, int index, int size, SortBy<User> sortBy)
        {
            Expression<Func<User, bool>> predicateWhere = GetAndSearchCondition(searchItem);
            IEnumerable<User> listUser = base.GetBy(index, size, sortBy, predicateWhere);
            return listUser;
        }
        public override IEnumerable<User> GetByOr(UserSearch searchItem, int index, int size, SortBy<User> sortBy)
        {
            Expression<Func<User, bool>> predicateWhere = GetOrSearchCondition(searchItem);
            IEnumerable<User> listUser = base.GetBy(index, size, sortBy, predicateWhere);
            return listUser;
        }
    }
}
