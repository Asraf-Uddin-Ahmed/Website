using System;
using Website.Foundation.Core.Aggregates;
namespace Website.Web.Codes.Service
{
    public interface IEmailService
    {
        void SendConfirmUser(User newUser, string url);
        void SendForgotPassword(User registeredUser, string url);
    }
}
