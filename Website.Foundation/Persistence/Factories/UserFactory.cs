using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Factories;

namespace Website.Foundation.Factories
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string userName, string email, string password, string name, UserType type, UserStatus status)
        {
            User user = new User();
            user.Name = name;
            user.TypeOfUser = type;
            user.UserName = userName;
            user.EmailAddress = email;
            user.Status = status;
            user.EncryptedPassword = CryptographicUtility.Encrypt(password, user.ID);

            user.CreationTime = DateTime.UtcNow;
            user.LastLogin = null;
            user.LastWrongPasswordAttempt = null;
            user.UpdateTime = DateTime.UtcNow;
            user.WrongPasswordAttempt = 0;
            return user;
        }
    }
}
