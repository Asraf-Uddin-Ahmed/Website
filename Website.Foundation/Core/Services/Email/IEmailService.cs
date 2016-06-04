using System;
using Website.Foundation.Core.Aggregates;
namespace Website.Foundation.Core.Services.Email
{
    // Director for MessageBuilder
    public interface IEmailService
    {
        void SendText(IMessageBuilder messageFactory);
        void SendHtml(IMessageBuilder messageFactory);
    }
}
