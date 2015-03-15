﻿using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Website.Foundation.Container;
using Website.Foundation.Helpers;
using Ratul.Utility;
using System.IO;

namespace Website.Foundation.Repositories
{
    public class UserRepository : BaseEfRepository<User>, IUserRepository
    {
        private TableContext _context;
        private IRepositorySearchHelper _repositorySearchHelper;
        [Inject]
        public UserRepository(TableContext context, IRepositorySearchHelper repositorySearchHelper)
            : base(context)
        {
            _context = context;
            _repositorySearchHelper = repositorySearchHelper;
        }

        public new void Add(IEntity entity)
        {
            User user = (User)entity;
            user.Password = CryptographicUtility.Encrypt(user.Password, user.ID);
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public new void Update(IEntity entity)
        {
            IUser currentUser = (IUser)Get(entity.ID);
            if (currentUser == null)
                return;
            currentUser.Password = CryptographicUtility.Encrypt(currentUser.Password, currentUser.ID);
            _context.Entry(currentUser).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
        public string ResetPassword(IUser user)
        {
            string password = Path.GetRandomFileName().Substring(0, 8);
            user.Password = password;
            this.Update(user);
            return password;
        }
        public string GetPassword(IUser user)
        {
            string password = CryptographicUtility.Decrypt(user.Password, user.ID);
            return password;
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
        

        private Func<IUser, bool> GetAndSearchCondition(UserSearch searchItem)
        {
            Func<IUser, bool> predicate = (col) =>
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
        private Func<IUser, bool> GetOrSearchCondition(UserSearch searchItem)
        {
            bool isAllNull = _repositorySearchHelper.IsAllPropertyNull(searchItem);
            Func<IUser, bool> predicate = (col) =>
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
        public int GetTotalAnd(UserSearch searchItem)
        {
            Func<IUser, bool> predicateCount = GetAndSearchCondition(searchItem);
            int total = base.GetTotalBy(predicateCount);
            return total;
        }
        public int GetTotalOr(UserSearch searchItem)
        {
            Func<IUser, bool> predicateCount = GetOrSearchCondition(searchItem);
            int total = base.GetTotalBy(predicateCount);
            return total;
        }

        public IEnumerable<IUser> GetPagedAnd(UserSearch searchItem, int pageNumber, int pageSize, Func<IUser, dynamic> predicateOrderBy)
        {
            Func<IUser, bool> predicateWhere = GetAndSearchCondition(searchItem);
            IEnumerable<IEntity> listUser = base.GetPagedBy(pageNumber, pageSize, predicateOrderBy, predicateWhere);
            return listUser.Cast<IUser>();
        }
        public IEnumerable<IUser> GetPagedOr(UserSearch searchItem, int pageNumber, int pageSize, Func<IUser, dynamic> predicateOrderBy)
        {
            Func<IUser, bool> predicateWhere = GetOrSearchCondition(searchItem);
            IEnumerable<IEntity> listUser = base.GetPagedBy(pageNumber, pageSize, predicateOrderBy, predicateWhere);
            return listUser.Cast<IUser>();
        }
    }
}
