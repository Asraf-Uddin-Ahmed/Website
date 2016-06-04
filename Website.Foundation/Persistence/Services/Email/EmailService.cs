﻿using Ratul.Utility.Email;
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
        private ISettingsRepository _settingsService;
        
        
        public EmailService(ISettingsRepository settingsService)
        {
            _settingsService = settingsService;
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
            emailSettings.Host = _settingsService.GetValueByName(SettingsName.EmailHost);
            emailSettings.UserName = _settingsService.GetValueByName(SettingsName.EmailUserName);
            emailSettings.Password = _settingsService.GetValueByName(SettingsName.EmailPassword);
            emailSettings.Port = int.Parse(_settingsService.GetValueByName(SettingsName.EmailPort));
            emailSettings.EnableSsl = bool.Parse(_settingsService.GetValueByName(SettingsName.EmailEnableSSL));
            _emailSender = new EmailSender(emailSettings);
        }

    }
}