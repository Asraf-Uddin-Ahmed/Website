using Newtonsoft.Json;
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
        private IUnitOfWork _unitOfWork;
        [Inject]
        public UserService(ILogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public bool IsEmailAddressAlreadyInUse(string email)
        {
            try
            {
                bool isExist = _unitOfWork.Users.IsEmailExist(email);
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
                bool isExist = _unitOfWork.Users.IsUserNameExist(userName);
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
                return _unitOfWork.Users.GetByUserName(userName);
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
                return _unitOfWork.Users.GetByEmail(email);
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
                return _unitOfWork.Users.Get(userID);
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
                List<User> result = _unitOfWork.Users.GetBy(index, size, sortBy).Cast<User>().ToList<User>();
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
                _unitOfWork.Users.Remove(userID);
                _unitOfWork.Commit();
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
                _unitOfWork.Users.Update(user);
                _unitOfWork.Commit();
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
                List<User> result = _unitOfWork.Users.GetAll().ToList<User>();
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
