using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Website.Foundation.Repositories
{
    public class UserVerificationRepository : BaseEfRepository<UserVerification>, IUserVerificationRepository
    {
        private TableContext _context;
        [Inject]
        public UserVerificationRepository(TableContext context)
            : base(context)
        {
            _context = context;
        }

        public IUserVerification GetByVerificationCode(string verificationCode)
        {
            IUserVerification userVerification = _context.UserVerifications.Where(col => col.VerificationCode == verificationCode).FirstOrDefault();
            return userVerification;
        }

        public bool IsVerificationCodeExist(string verificationCode)
        {
            bool isExist = _context.UserVerifications.Any(col => col.VerificationCode == verificationCode);
            return isExist;
        }

        public void RemoveByVerificationCode(string verificationCode)
        {
            IUserVerification userVerification = GetByVerificationCode(verificationCode);
            Remove(userVerification);
        }
        
    }
}
