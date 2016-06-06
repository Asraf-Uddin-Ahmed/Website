using Ratul.Utility.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services.Email;
using Website.Foundation.Persistence.Template;

namespace Website.Foundation.Persistence.Services.Email
{
    public class EmailService : IEmailService
    {
        private EmailSender _emailSender;
        private ISettingsRepository _settingsRepository;
        
        
        public EmailService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            this.InitializeEmailSender();
        }



        public void SendText(IMessageBuilder messageFactory)
        {
            _emailSender.Send(messageFactory.GetText());
        }
        public void SendHtml(IMessageBuilder messageFactory)
        {
            _emailSender.Send(messageFactory.GetHtml());
        }



        private void InitializeEmailSender()
        {
            EmailSettings emailSettings = new EmailSettings();
            emailSettings.Host = _settingsRepository.GetValueByName(SettingsName.EmailHost);
            emailSettings.UserName = _settingsRepository.GetValueByName(SettingsName.EmailUserName);
            emailSettings.Password = _settingsRepository.GetValueByName(SettingsName.EmailPassword);
            emailSettings.Port = int.Parse(_settingsRepository.GetValueByName(SettingsName.EmailPort));
            emailSettings.EnableSsl = bool.Parse(_settingsRepository.GetValueByName(SettingsName.EmailEnableSSL));
            _emailSender = new EmailSender(emailSettings);
        }

    }
}