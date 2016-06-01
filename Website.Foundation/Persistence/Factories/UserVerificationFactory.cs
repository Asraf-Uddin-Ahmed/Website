using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Factories;

namespace Website.Foundation.Persistence.Factories
{
    public class UserVerificationFactory : IUserVerificationFactory
    {
        public UserVerification Create()
        {
            UserVerification userVerification = new UserVerification();
            userVerification.ID = GuidUtility.GetNewSequentialGuid();
            userVerification.CreationTime = DateTime.UtcNow;
            userVerification.VerificationCode = UserUtility.GetNewVerificationCode();
            return userVerification;
        }
    }
}
