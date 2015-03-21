using System;
namespace Website.Web.Codes
{
    public interface IMembershipService
    {
        Website.Foundation.Aggregates.IUser CreateUser(Website.Foundation.Container.UserCreationData data);
    }
}
