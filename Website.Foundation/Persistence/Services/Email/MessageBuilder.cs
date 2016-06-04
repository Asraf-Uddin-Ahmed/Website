﻿using Ratul.Utility.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services.Email;

namespace Website.Foundation.Persistence.Services.Email
{
    public abstract class MessageBuilder : IMessageBuilder
    {
        private ISettingsRepository _settingsService;
        public MessageBuilder(ISettingsRepository settingsService)
        {
            _settingsService = settingsService;
        }

        public MessageSettings GetText()
        {
            MessageSettings messageSettings = this.GetMessageSettings();
            messageSettings.IsBodyHtml = false;
            return messageSettings;
        }

        public MessageSettings GetHtml()
        {
            MessageSettings messageSettings = this.GetMessageSettings();
            messageSettings.IsBodyHtml = true;
            return messageSettings;
        }



        protected abstract string GetSubject();
        protected abstract string GetBody();
        protected abstract NameWithEmail GetFrom();
        protected abstract List<NameWithEmail> GetToList();
        protected abstract List<NameWithEmail> GetReplyToList();
        protected NameWithEmail GetSystemNameWithEmail()
        {
            string systemEmail = _settingsService.GetValueByName(SettingsName.SystemEmailAddress);
            string systemName = _settingsService.GetValueByName(SettingsName.SystemEmailName);
            NameWithEmail systemNameWithEmail = new NameWithEmail(systemName, systemEmail);
            return systemNameWithEmail;
        }



        private MessageSettings GetMessageSettings()
        {
            MessageSettings messageSettings = new MessageSettings();
            messageSettings.Subject = this.GetSubject();
            messageSettings.Body = this.GetBody();
            messageSettings.From = this.GetFrom();
            messageSettings.ReplyToList = this.GetReplyToList();
            messageSettings.ToList = this.GetToList();
            return messageSettings;
        }
        
    }
}
