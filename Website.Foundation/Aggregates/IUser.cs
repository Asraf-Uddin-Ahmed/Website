using System;
namespace Website.Foundation.Aggregates
{
    public interface IUser
    {
        DateTime CreationTime { get; set; }
        string EmailAddress { get; set; }
        DateTime? LastLogin { get; set; }
        DateTime? LastWrongPasswordAttempt { get; set; }
        string Password { get; set; }
        Website.Foundation.Enums.UserStatus Status { get; set; }
        Website.Foundation.Enums.UserType TypeOfUser { get; set; }
        DateTime UpdateTime { get; set; }
        string UserName { get; set; }
        int WrongPasswordAttempt { get; set; }
    }
}
