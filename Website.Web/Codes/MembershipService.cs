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
        private ISettingsRepository _settingsRepository;
        [Inject]
        public MembershipService(ILogger logger,
            IUserFactory userFactory,
            IUserRepository userRepository,
            ISettingsRepository settingsRepository)
        {
            _logger = logger;
            _userFactory = userFactory;
            _userRepository = userRepository;
            _settingsRepository = settingsRepository;
        }

        public IUser CreateUser(UserCreationData data)
        {
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

        ///// <summary>
        ///// Checks whether the email address is already used by any registered user
        ///// </summary>
        ///// <param name="email">The email to check</param>
        ///// <returns>If the email is already in use, it returns true, otherwise it returns false</returns>
        //public bool IsEmailAddressAlreadyInUse(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //        throw new ArgumentException("Email address is missing");
        //    if (!_regExUtil.IsEmail(email))
        //        throw new ArgumentException("Email address is invalid");
        //    try
        //    {
        //        IUser user = _userRepository.GetByEmail(email);
        //        if (user != null)
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "CreateUser", ex,
        //            string.Format("Failed to check email address: email={0}", email));

        //        throw;
        //    }
        //    return false;
        //}

        //public bool IsUsernameAlreadyInUse(string username)
        //{
        //    try
        //    {
        //        IUser user = _userRepository.GetByUsername(username);
        //        return user != null;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "IsUsernameAlreadyInUse", ex,
        //            string.Format("Failed to get user by username with parameters: username={0}", username));

        //        return false;
        //    }
        //}

        ///// <summary>
        ///// If the verification code is found, the user email associated with the verification
        ///// code will be moved from Unverified to Active. But if the user is Blocked, then
        ///// no change will occure.
        ///// </summary>
        ///// <param name="verificationCode">The verification code sent in email</param>
        ///// <returns>If the verification process is successful returns true, otherwise false.</returns>
        //public AccountVerificationStatus VerifyUser(string verificationCode)
        //{
        //    if (string.IsNullOrEmpty(verificationCode))
        //        throw new ArgumentException("Verification code is missing");

        //    try
        //    {
        //        IUserVerification verification = GetVerification(verificationCode);
        //        if (verification != null)
        //        {
        //            if (DateTime.UtcNow.Subtract(verification.CreatedOn).TotalDays <= 7)
        //            {
        //                IUser user = GetUser(verification.UserID);
        //                if (user != null)
        //                {
        //                    user.Status = UserStatus.Active;
        //                    _unitOfwork.MarkDirty(user);
        //                    _unitOfwork.Commit();
        //                    return AccountVerificationStatus.Success;
        //                }
        //                else
        //                    return AccountVerificationStatus.VerificationCodeDoesNotExist;
        //            }
        //            else
        //                return AccountVerificationStatus.VerificationCodeExpired;
        //        }
        //        else
        //            return AccountVerificationStatus.VerificationCodeDoesNotExist;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "VerifyUser",
        //            ex, "Failed to verify user with verificationCode : " + verificationCode);

        //        return AccountVerificationStatus.Fail;
        //    }
        //}

        //public IUserVerification GetVerification(string verificationCode)
        //{
        //    if (string.IsNullOrEmpty(verificationCode))
        //        throw new ArgumentException("Verification code is missing");

        //    try
        //    {
        //        return _userVerificationRepository.GetByVerificationCode(verificationCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "GetVerification",
        //            ex, "Failed to get verification with verificationCode: " + verificationCode);

        //        return null;
        //    }
        //}

        //public IUser GetUserByUsername(string username)
        //{
        //    if (string.IsNullOrEmpty(username))
        //        throw new ArgumentException("Username is missing");

        //    try
        //    {
        //        return _userRepository.GetByUsername(username);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "UserLogin", ex,
        //            string.Format("Failed to get user by Username with parameter: usernmae={0}", username));

        //        return null;
        //    }
        //}

        //public IUser GetUserByEmail(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //        throw new ArgumentException("Email address is missing");
        //    if (!_regExUtil.IsEmail(email))
        //        throw new ArgumentException("Email address is invalid");

        //    try
        //    {
        //        return _userRepository.GetByEmail(email);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "GetUserByEmail",
        //            ex, "Failed to get user by email with email: " + email);

        //        return null;
        //    }
        //}

        //public IUser GetUser(Guid userID)
        //{
        //    try
        //    {
        //        return (IUser)_userRepository.Get(userID);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "GetUser", ex,
        //            string.Format("Failed to get user with parameters: userID={0}", userID));

        //        return null;
        //    }
        //}

        //public IUser GetUser(IUser user, Guid userID)
        //{
        //    try
        //    {
        //        return (IUser)_userRepository.Get(userID, user);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "GetUser", ex,
        //            string.Format("Failed to get user with parameters: userID={0}", userID));

        //        return null;
        //    }
        //}


        //public ICollection<IUser> GetUsers(UserSearch search, int pageIndex, int pageSize, ICollection<SortElement> sorts)
        //{
        //    if (search == null)
        //        throw new ArgumentNullException("search", "Search parameter is missing");
        //    if (pageIndex < 0 || pageSize < 0)
        //        throw new ArgumentException("pageIndex", "PagedIndex in negetive");
        //    if (sorts == null)
        //        throw new ArgumentNullException("sorts", "sorts parameter is missing");

        //    try
        //    {
        //        var result = _userRepository.GetAllPaged(pageIndex, pageSize, search, sorts).Cast<IUser>().ToList<IUser>();

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "GetUsers",
        //            ex, string.Format("Failed to Get Users with values: search={0}, pageIndex={1}, pageSize={2}, sort={3}",
        //            JsonConvert.SerializeObject(search), pageIndex, pageSize, JsonConvert.SerializeObject(sorts)));

        //        return null;
        //    }
        //}

        //public bool BlockUser(Guid userID)
        //{
        //    try
        //    {
        //        IUser user = GetUser(userID);

        //        if (user == null)
        //            return false;

        //        user.Status = UserStatus.Blocked;

        //        _unitOfwork.MarkDirty(user);
        //        _unitOfwork.Commit();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "BlockUser",
        //            ex, string.Format("Failed to Block User with values: userID={0}", userID));

        //        return false;
        //    }
        //}

        //public bool UnblockUser(Guid userID)
        //{
        //    try
        //    {
        //        IUser user = GetUser(userID);

        //        if (user == null)
        //            return false;

        //        user.Status = UserStatus.Active;

        //        _unitOfwork.MarkDirty(user);
        //        _unitOfwork.Commit();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "UnblockUser",
        //            ex, string.Format("Failed to Un-Block User with values: userID={0}", userID));

        //        return false;
        //    }
        //}

        //public bool DeleteUser(Guid userID)
        //{
        //    try
        //    {
        //        IUser user = GetUser(userID);

        //        if (user == null)
        //            return false;

        //        _unitOfwork.MarkDeleted(user);
        //        _unitOfwork.Commit();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "DeleteUser",
        //            ex, string.Format("Failed to Delete User with values: userID={0}", userID));

        //        return false;
        //    }
        //}

        //public bool ChangeUserPassword(Guid userID, string oldPassword, string newPassword)
        //{
        //    if (userID == Guid.Empty)
        //        throw new ArgumentException("userID is invalid");
        //    if (string.IsNullOrEmpty(oldPassword))
        //        throw new ArgumentNullException("oldPassword");
        //    if (string.IsNullOrEmpty(newPassword))
        //        throw new ArgumentNullException("newPassword");

        //    try
        //    {
        //        IUser user = GetUser(userID);

        //        if (user == null)
        //            return false;

        //        user.SetPassword(newPassword, oldPassword);
        //        _unitOfwork.MarkDirty(user);
        //        _unitOfwork.Commit();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "ChangeUserPassword",
        //            ex, string.Format("Failed to ChangeUserPassword with values: userID={0}, oldPassword={1}, newPassword={2}",
        //            userID, oldPassword, newPassword));

        //        return false;
        //    }

        //}

        //public bool UpdateUserInformation(IUser user)
        //{
        //    if (user == null)
        //        return false;

        //    try
        //    {
        //        _unitOfwork.MarkDirty(user);
        //        _unitOfwork.Commit();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.Create().WriteLog(LogType.HandledLog, this.GetType().Name, "UpdateUserInformation",
        //            ex, string.Format("Failed to UpdateUserInformation with values: user={0}", JsonConvert.SerializeObject(user)));

        //        return false;
        //    }
        //}


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