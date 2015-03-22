using System;
using Website.Foundation.Aggregates;
namespace Website.Foundation.Repositories
{
    public interface IPasswordVerificationRepository : IBaseEfRepository<PasswordVerification>
    {
        Website.Foundation.Aggregates.IPasswordVerification GetByVerificationCode(string verificationCode);
        bool IsVerificationCodeExist(string verificationCode);
        void RemoveByUserID(Guid userID);
        void RemoveByVerificationCode(string verificationCode);
    }
}
