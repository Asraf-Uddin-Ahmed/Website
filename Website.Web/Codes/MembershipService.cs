using Newtonsoft.Json;
using Ninject;
using Ninject.Extensions.Logging;
using Ratul.Mvc;
using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Foundation;
using Website.Foundation.Aggregates;
using Website.Foundation.Container;
using Website.Foundation.Enums;
using Website.Foundation.Factory;
using Website.Foundation.Repositories;
using Website.Web.App_Start;

namespace Website.Web.Codes
{
    public class MembershipService : IMembershipService
    {
        private ILogger _logger;
        private IUserFactory _userFactory;
        private IUserRepository _userRepository;
        private IUserVerificationRepository _userVerificationRepository;
        private IRegexUtility _regexUtility;
        private ISettingsRepository _settingsRepository;
        [Inject]
        public MembershipService(ILogger logger,
            IUserFactory userFactory,
            IUserRepository userRepository,
            IUserVerificationRepository userVerificationRepository,
            IRegexUtility regexUtility,
            ISettingsRepository settingsRepository)
        {
            _logger = logger;
            _userFactory = userFactory;
            _userRepository = userRepository;
            _userVerificationRepository = userVerificationRepository;
            _regexUtility = regexUtility;
            _settingsRepository = settingsRepository;
        }

        public IUser CreateUser(UserCreationData data)
        {
            if (data == null)
                throw new ArgumentException("UserCreationData is missing");
            if (string.IsNullOrEmpty(data.Email))
                throw new ArgumentException("Email address is missing");
            if (!_regexUtility.IsEmailValid(data.Email))
                throw new ArgumentException("Email address is invalid");
            if (string.IsNullOrEmpty(data.UserName))
                throw new ArgumentException("UserName is missing");
            if (string.IsNullOrEmpty(data.Password))
                throw new ArgumentException("Password is missing");
            
            try
            {
                IUser user = _userFactory.CreateUser(data.UserName, data.Email, data.Password, data.Name, data.TypeOfUser, UserStatus.Active);
                if (data.HasVerificationCode)
                {
                    user.Status = UserStatus.Unverified;

                    IUserVerification userVerification = NinjectWebCommon.GetConcreteInstance<IUserVerification>();
                    userVerification.CreationTime = DateTime.UtcNow;
                    userVerification.VerificationCode = UserUtility.GetNewVerificationCode();

                    user.UserVerifications = new List<UserVerification>();
                    user.UserVerifications.Add((UserVerification)userVerification);
                }
                _userRepository.Add(user);
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create user with parameters: usernmae={0}, mobile={1}, email={2}", data.UserName, data.Email, data.TypeOfUser);
                return null;
            }
        }

        public LoginStatus ProcessLogin(string userName, string password)
        {
            try
            {
                IUser user = _userRepository.GetByUserName(userName);
                if (user == null)
                    return LoginStatus.InvalidLogin;

                if (!user.DecryptedPassword.Equals(password))
                {
                    ProcessInvalidLogin(user);
                    return LoginStatus.InvalidLogin;
                }

                if (user.Status == UserStatus.Active)
                {
                    ProcessValidLogin(user);
                    return LoginStatus.Successful;
                }
                if (user.Status == UserStatus.Blocked)
                    return LoginStatus.Blocked;
                if (user.Status == UserStatus.Unverified)
                    return LoginStatus.Unverified;
                return LoginStatus.InvalidLogin;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to login with the following parameters : username={0}, password={1}", userName, password);
                return LoginStatus.Failed;
            }
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

        /// <summary>
        /// If the verification code is found, the user email associated with the verification
        /// code will be moved from Unverified to Active. But if the user is Blocked, then
        /// no change will occure.
        /// </summary>
        /// <param name="verificationCode">The verification code sent in email</param>
        /// <returns></returns>
        public AccountVerificationStatus VerifyUser(string verificationCode)
        {
            if (string.IsNullOrEmpty(verificationCode))
                throw new ArgumentException("Verification code is missing");

            try
            {
                IUserVerification verification = _userVerificationRepository.GetByVerificationCode(verificationCode);
                if (verification == null)
                    return AccountVerificationStatus.VerificationCodeDoesNotExist;

                IUser user = GetUser(verification.UserID);
                if (user != null && user.Status != UserStatus.Blocked)
                {
                    user.Status = UserStatus.Active;
                    _userRepository.Update(user);
                    _userVerificationRepository.RemoveByUserID(user.ID);
                    return AccountVerificationStatus.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to verify user with verificationCode: {0}", verificationCode);
            }
            return AccountVerificationStatus.Fail;
        }

        public IUserVerification GetUserVerification(string verificationCode)
        {
            if (string.IsNullOrEmpty(verificationCode))
                throw new ArgumentException("Verification code is missing");

            try
            {
                return _userVerificationRepository.GetByVerificationCode(verificationCode);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get verification with verificationCode: {0}", verificationCode);
                return null;
            }
        }

        public IUser GetUserByUserName(string userName)
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

        public IUser GetUserByEmail(string email)
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

        public IUser GetUser(Guid userID)
        {
            try
            {
                return (IUser)_userRepository.Get(userID);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get user with parameters: userID={0}", userID);
                return null;
            }
        }


        public ICollection<IUser> GetUsers(UserSearch search, int pageNumber, int pageSize, Func<IUser, dynamic> orderBy)
        {
            if (search == null)
                throw new ArgumentNullException("search", "Search parameter is missing");
            if (pageNumber < 0 || pageSize < 0)
                throw new ArgumentException("Invalid pageNumber and pageSize");
            
            try
            {
                List<IUser> result = _userRepository.GetPagedAnd(search, pageNumber, pageSize, orderBy).Cast<IUser>().ToList<IUser>();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Get Users with values: search={0}, pageNumber={1}, pageSize={2}, sort={3}",
                    JsonConvert.SerializeObject(search), pageNumber, pageSize, JsonConvert.SerializeObject(orderBy));
                return null;
            }
        }

        public bool BlockUser(Guid userID)
        {
            try
            {
                IUser user = GetUser(userID);
                if (user == null)
                    return false;
                user.Status = UserStatus.Blocked;
                _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Block User with userID: {0}", userID);
                return false;
            }
        }

        public bool UnblockUser(Guid userID)
        {
            try
            {
                IUser user = GetUser(userID);
                if (user == null)
                    return false;
                user.Status = UserStatus.Active;
                _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Un-Block User with userID: {0}", userID);
                return false;
            }
        }

        public bool DeleteUser(Guid userID)
        {
            try
            {
                _userRepository.Remove(userID);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Delete User with userID: {0}", userID);
                return false;
            }
        }

        public bool ChangeUserPassword(Guid userID, string oldPassword, string newPassword)
        {
            if (userID == Guid.Empty)
                throw new ArgumentException("userID is invalid");
            if (string.IsNullOrEmpty(oldPassword))
                throw new ArgumentNullException("oldPassword");
            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException("newPassword");

            try
            {
                IUser user = GetUser(userID);
                if (user == null || user.DecryptedPassword != oldPassword)
                    return false;
                
                user.EncryptedPassword = CryptographicUtility.Encrypt(newPassword, user.ID);
                _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to ChangeUserPassword with values: userID={0}, oldPassword={1}, newPassword={2}", userID, oldPassword, newPassword);
                return false;
            }

        }

        public bool UpdateUserInformation(IUser user)
        {
            if (user == null)
                return false;
            try
            {
                _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to UpdateUserInformation with values: user={0}", JsonConvert.SerializeObject(user));
                return false;
            }
        }


        private void ProcessInvalidLogin(IUser user)
        {
            user.LastWrongPasswordAttempt = DateTime.UtcNow;
            user.WrongPasswordAttempt++;
            int maxPasswordMistake = int.Parse(_settingsRepository.GetValueByName("max_password_mistake"));
            if (user.WrongPasswordAttempt > maxPasswordMistake)
                user.Status = UserStatus.Blocked;
            _userRepository.Update(user);
        }
        public void ProcessValidLogin(IUser user)
        {
            user.LastLogin = DateTime.UtcNow;
            user.WrongPasswordAttempt = 0;
            _userRepository.Update(user);
            UserSession.CurrentUser = new UserIdentity(user.ID, user.TypeOfUser.ToString(), user.Name);
        }
    }
}