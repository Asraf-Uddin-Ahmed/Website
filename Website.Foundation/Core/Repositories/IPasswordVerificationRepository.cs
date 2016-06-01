using System;
using Website.Foundation.Core.Aggregates;
namespace Website.Foundation.Core.Repositories
{
    public interface IPasswordVerificationRepository : IRepository<PasswordVerification>
    {
        PasswordVerification GetByVerificationCode(string verificationCode);
        bool IsVerificationCodeExist(string verificationCode);
        void RemoveByUserID(Guid userID);
        void RemoveByVerificationCode(string verificationCode);
    }
}
