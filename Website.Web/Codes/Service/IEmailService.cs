using System;
namespace Website.Web.Codes.Service
{
    public interface IEmailService
    {
        void SendConfirmUser(Website.Foundation.Aggregates.IUser newUser, string url);
        void SendForgotPassword(Website.Foundation.Aggregates.IUser registeredUser, string url);
    }
}
