using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Persistence.Repositories;
using Website.Foundation.Persistence;

namespace Website.Foundation.Persistence.Repositories
{
    public class PasswordVerificationRepository : Repository<PasswordVerification>, IPasswordVerificationRepository
    {
        private ApplicationDbContext _context;
        [Inject]
        public PasswordVerificationRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public PasswordVerification GetByVerificationCode(string verificationCode)
        {
            PasswordVerification passwordVerification = _context.PasswordVerifications.Where(col => col.VerificationCode == verificationCode).FirstOrDefault();
            return passwordVerification;
        }

        public bool IsVerificationCodeExist(string verificationCode)
        {
            bool isExist = _context.PasswordVerifications.Any(col => col.VerificationCode == verificationCode);
            return isExist;
        }

        public void RemoveByVerificationCode(string verificationCode)
        {
            PasswordVerification passwordVerification = GetByVerificationCode(verificationCode);
            base.Remove(passwordVerification);
        }
        public void RemoveByUserID(Guid userID)
        {
            base.RemoveRange(_context.PasswordVerifications.Where(c => c.UserID == userID));
        }
    }
}
