using System;
using Website.Foundation.Core.Aggregates;
namespace Website.Foundation.Core.Services
{
    public interface IEmailService
    {
        void SendConfirmUser(User newUser, string url);
        void SendForgotPassword(User registeredUser, string url);
    }
}
