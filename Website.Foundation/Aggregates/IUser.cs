using System;
using Website.Foundation.Enums;
namespace Website.Foundation.Aggregates
{
    public interface IUser
    {
        DateTime CreationTime { get; set; }
        string EmailAddress { get; set; }
        DateTime? LastLogin { get; set; }
        DateTime? LastWrongPasswordAttempt { get; set; }
        string Password { get; set; }
        UserStatus Status { get; set; }
        UserType TypeOfUser { get; set; }
        DateTime UpdateTime { get; set; }
        string UserName { get; set; }
        int WrongPasswordAttempt { get; set; }
    }
}
