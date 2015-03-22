﻿using System;
namespace Website.Foundation.Aggregates
{
    public interface IPasswordVerification : IEntity
    {
        DateTime CreationTime { get; set; }
        User User { get; set; }
        Guid UserID { get; set; }
        string VerificationCode { get; set; }
    }
}
