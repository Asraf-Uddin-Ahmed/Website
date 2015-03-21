using System;
namespace Website.Foundation.Factory
{
    public interface IUserFactory
    {
        Website.Foundation.Aggregates.IUser CreateUser(string userName, string email, string password, string name, Website.Foundation.Enums.UserType type, Website.Foundation.Enums.UserStatus status);
    }
}
