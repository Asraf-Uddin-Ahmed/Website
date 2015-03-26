using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Ratul.Utility.Email
{
    public class EmailSender
    {
        private SmtpClient _smtpClient;
        public EmailSender(EmailSettings settings)
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Host = settings.Host;
            _smtpClient.Port = settings.Port;
            _smtpClient.EnableSsl = settings.EnableSsl;
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new NetworkCredential(settings.UserName, settings.Password);
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public bool Send(MessageSettings settings)
        {
            try
            {
                MailMessage message = new MailMessage();
                settings.SetMailAddressCollectionForToList(message.To);
                message.From = new MailAddress(settings.From.EmailAddress, settings.From.Name);
                settings.SetMailAddressCollectionForReplyToList(message.ReplyToList);
                message.Subject = settings.Subject;
                message.Body = settings.Body;
                message.IsBodyHtml = settings.IsBodyHtml;
                message.Priority = MailPriority.High;
                _smtpClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
