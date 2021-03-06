﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;

namespace Website.Foundation.Core.Repositories
{
    public interface IUserVerificationRepository : IRepository<UserVerification>
    {
        UserVerification GetByVerificationCode(string verificationCode);
        bool IsVerificationCodeExist(string verificationCode);
        void RemoveByVerificationCode(string verificationCode);
        void RemoveByUserID(Guid userID);
    }
}
