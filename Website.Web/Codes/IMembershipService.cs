using System;
using Website.Foundation.Enums;
namespace Website.Web.Codes
{
    public interface IMembershipService
    {
        Website.Foundation.Aggregates.IUser CreateUser(Website.Foundation.Container.UserCreationData data);
        LoginStatus ProcessLogin(string userName, string password);
        bool IsEmailAddressAlreadyInUse(string email);
        bool IsUserNameAlreadyInUse(string userName);
    }
}
