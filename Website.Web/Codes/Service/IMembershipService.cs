﻿using System;
using Website.Foundation.Core.Aggregates;
namespace Website.Web.Codes.Service
{
    public interface IMembershipService
    {
        bool BlockUser(Guid userID);
        bool ChangeUserPassword(Guid userID, string newPassword);
        bool ChangeUserPassword(Guid userID, string oldPassword, string newPassword);
        User CreateUser(Website.Foundation.Core.Container.UserCreationData data);
        Website.Foundation.Core.Enums.LoginStatus ProcessLogin(string userName, string password);
        bool UnblockUser(Guid userID);
        Website.Foundation.Core.Enums.VerificationStatus VerifyForPasswordChange(string verificationCode);
        Website.Foundation.Core.Enums.VerificationStatus VerifyForUserStatus(string verificationCode);
        PasswordVerification ProcessForgotPassword(User user);
    }
}
