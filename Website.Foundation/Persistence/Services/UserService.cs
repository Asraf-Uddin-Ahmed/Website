﻿using Newtonsoft.Json;
using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Container;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services;

namespace Website.Foundation.Persistence.Services
{
    public class UserService : IUserService
    {
        private ILogger _logger;
        private IUserRepository _userRepository;
        [Inject]
        public UserService(ILogger logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public bool IsEmailAddressAlreadyInUse(string email)
        {
            try
            {
                bool isExist = _userRepository.IsEmailExist(email);
                return isExist;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to check email address: email={0}", email);
                return false;
            }
        }

        public bool IsUserNameAlreadyInUse(string userName)
        {
            try
            {
                bool isExist = _userRepository.IsUserNameExist(userName);
                return isExist;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get user by username with parameters: username={0}", userName);
                return false;
            }
        }

        public User GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Username is missing");

            try
            {
                return _userRepository.GetByUserName(userName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get user by username with UserName : {0}", userName);
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return _userRepository.GetByEmail(email);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get user by email with Email: {0}", email);
                return null;
            }
        }

        public User GetUser(Guid userID)
        {
            try
            {
                return _userRepository.Get(userID);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get user with userID: {0}", userID);
                return null;
            }
        }


        public ICollection<User> GetAllUserPaged(int index, int size, SortBy<User> sortBy)
        {
            if (index < 0 || size < 0)
                throw new ArgumentException("Invalid index and size");

            try
            {
                List<User> result = _userRepository.GetBy(index, size, sortBy).Cast<User>().ToList<User>();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Get Users with values: index={0}, size={1}, sort={2}",
                    index, size, JsonConvert.SerializeObject(sortBy));
                return null;
            }
        }
        public bool DeleteUser(Guid userID)
        {
            try
            {
                // use '_userRepository.Commit()' function after 'remove' if you do not use 'isPersist' value 'true'.
                // See 'UpdateUserInformation' for detail.
                _userRepository.Remove(userID, true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Delete User with userID: {0}", userID);
                return false;
            }
        }
        public bool UpdateUserInformation(User user)
        {
            if (user == null)
                return false;
            try
            {
                // You can also give the 'isPersist' value 'true' for instant save.
                // See 'DeleteUser' for detail.
                _userRepository.Update(user);
                _userRepository.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to UpdateUserInformation with values: user={0}", JsonConvert.SerializeObject(user));
                return false;
            }
        }

        public ICollection<User> GetAll()
        {
            try
            {
                List<User> result = _userRepository.GetAll().ToList<User>();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Get All Users");
                return new List<User>();
            }
        }
    }
}
