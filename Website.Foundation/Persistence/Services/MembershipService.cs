﻿using Newtonsoft.Json;
using Ninject;
using Ninject.Extensions.Logging;
using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Foundation;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Container;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Factories;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services;

namespace Website.Foundation.Persistence.Services
{
    public class MembershipService : IMembershipService
    {
        private ILogger _logger;
        private IPasswordVerificationRepository _passwordVerificationRepository;
        private IUserFactory _userFactory;
        private IUserRepository _userRepository;
        private IUserService _userService;
        private IUserVerificationRepository _userVerificationRepository;
        private RegexUtility _regexUtility;
        private ISettingsRepository _settingsRepository;
        [Inject]
        public MembershipService(ILogger logger,
            IPasswordVerificationRepository passwordVerificationRepository,
            IUserFactory userFactory,
            IUserRepository userRepository,
            IUserService userService,
            IUserVerificationRepository userVerificationRepository,
            RegexUtility regexUtility,
            ISettingsRepository settingsRepository)
        {
            _logger = logger;
            _passwordVerificationRepository = passwordVerificationRepository;
            _userFactory = userFactory;
            _userRepository = userRepository;
            _userService = userService;
            _userVerificationRepository = userVerificationRepository;
            _regexUtility = regexUtility;
            _settingsRepository = settingsRepository;
        }

        public User CreateUser(User user)
        {
            if (user == null)
                throw new ArgumentException("User is missing");
            if (string.IsNullOrEmpty(user.EmailAddress))
                throw new ArgumentException("EmailAddress address is missing");
            if (!_regexUtility.IsEmailValid(user.EmailAddress))
                throw new ArgumentException("EmailAddress address is invalid");
            if (string.IsNullOrEmpty(user.UserName))
                throw new ArgumentException("UserName is missing");
            if (string.IsNullOrEmpty(user.EncryptedPassword))
                throw new ArgumentException("Password is missing");
            
            try
            {
                if (user.Status == UserStatus.Unverified)
                {
                    UserVerification userVerification = new UserVerification();//NinjectWebCommon.GetConcreteInstance<UserVerification>();
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
                _logger.Error(ex, "Failed to create user with parameters: UserName={0}, EmailAddress={1}, TypeOfUser={2}", user.UserName, user.EmailAddress, user.TypeOfUser);
                return null;
            }
        }

        public LoginStatus ProcessLogin(string userName, string password)
        {
            try
            {
                User user = _userService.GetUserByUserName(userName);
                if (user == null)
                    return LoginStatus.InvalidLogin;

                if (!user.DecryptedPassword.Equals(password))
                {
                    this.ProcessInvalidLogin(user);
                    return LoginStatus.InvalidLogin;
                }

                if (user.Status == UserStatus.Active)
                {
                    this.ProcessValidLogin(user);
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

        /// <summary>
        /// If the verification code is found, the user email associated with the verification
        /// code will be moved from Unverified to Active. But if the user is Blocked, then
        /// no change will occure. All user verification code will removed after successful activation.
        /// </summary>
        /// <param name="verificationCode">The verification code sent in email</param>
        /// <returns></returns>
        public VerificationStatus VerifyForUserStatus(string verificationCode)
        {
            if (string.IsNullOrEmpty(verificationCode))
                throw new ArgumentException("Verification code is missing");

            try
            {
                UserVerification verification = _userVerificationRepository.GetByVerificationCode(verificationCode);
                if (verification == null)
                    return VerificationStatus.VerificationCodeDoesNotExist;

                User user = _userService.GetUser(verification.UserID);
                if (user != null && user.Status != UserStatus.Blocked)
                {
                    user.Status = UserStatus.Active;
                    _userService.UpdateUserInformation(user);
                    _userVerificationRepository.RemoveByUserID(user.ID);
                    return VerificationStatus.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to verify user with verificationCode: {0}", verificationCode);
            }
            return VerificationStatus.Fail;
        }
        public PasswordVerification ProcessForgotPassword(User user)
        {
            PasswordVerification verification = new PasswordVerification(); // NinjectWebCommon.GetConcreteInstance<PasswordVerification>();
            verification.CreationTime = DateTime.UtcNow;
            verification.UserID = user.ID;
            verification.VerificationCode = UserUtility.GetNewVerificationCode();
            _passwordVerificationRepository.Add(verification);
            return verification;
        }
        /// <summary>
        /// If the verification code is found and the user is notBlocked, then
        /// all user verification code will removed before returning success.
        /// </summary>
        /// <param name="verificationCode">The verification code sent in email</param>
        /// <returns></returns>
        public VerificationStatus VerifyForPasswordChange(string verificationCode)
        {
            if (string.IsNullOrEmpty(verificationCode))
                throw new ArgumentException("Verification code is missing");

            try
            {
                PasswordVerification verification = _passwordVerificationRepository.GetByVerificationCode(verificationCode);
                if (verification == null)
                {
                    return VerificationStatus.VerificationCodeDoesNotExist;
                }

                User user = _userService.GetUser(verification.UserID);
                if (user != null)
                {
                    return VerificationStatus.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to verify user with verificationCode: {0}", verificationCode);
            }
            return VerificationStatus.Fail;
        }
        public bool BlockUser(Guid userID)
        {
            try
            {
                User user = _userService.GetUser(userID);
                if (user == null)
                    return false;
                user.Status = UserStatus.Blocked;
                _userService.UpdateUserInformation(user);
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
                User user = _userService.GetUser(userID);
                if (user == null)
                    return false;
                user.Status = UserStatus.Active;
                _userService.UpdateUserInformation(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Un-Block User with userID: {0}", userID);
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
                User user = _userService.GetUser(userID);
                if (user == null || user.DecryptedPassword != oldPassword)
                    return false;
                
                user.EncryptedPassword = CryptographicUtility.Encrypt(newPassword, user.ID);
                _userService.UpdateUserInformation(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to ChangeUserPassword with values: userID={0}, oldPassword={1}, newPassword={2}", userID, oldPassword, newPassword);
                return false;
            }

        }
        public bool ChangeUserPassword(Guid userID, string newPassword)
        {
            if (userID == Guid.Empty)
                throw new ArgumentException("userID is invalid");
            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException("newPassword");

            try
            {
                User user = _userService.GetUser(userID);
                if (user == null)
                    return false;

                user.EncryptedPassword = CryptographicUtility.Encrypt(newPassword, user.ID);
                _userService.UpdateUserInformation(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to ChangeUserPassword with values: userID={0}, newPassword={1}", userID, newPassword);
                return false;
            }

        }
        private void ProcessInvalidLogin(User user)
        {
            user.LastWrongPasswordAttempt = DateTime.UtcNow;
            user.WrongPasswordAttempt++;
            int maxPasswordMistake = int.Parse(_settingsRepository.GetValueByName(SettingsName.MaxPasswordMistake));
            if (user.WrongPasswordAttempt > maxPasswordMistake)
                user.Status = UserStatus.Blocked;
            _userService.UpdateUserInformation(user);
        }
        private void ProcessValidLogin(User user)
        {
            user.LastLogin = DateTime.UtcNow;
            user.WrongPasswordAttempt = 0;
            _userService.UpdateUserInformation(user);
        }
        
    }
}