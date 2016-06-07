using Microsoft.AspNet.Identity;
using Ninject;
using Ratul.Utility.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services.Email;
using Website.Foundation.Persistence;
using Website.Foundation.Persistence.Repositories;
using Website.Foundation.Persistence.Services.Email;

namespace Website.Foundation.Core.Identity
{
    public class EmailServiceProvider : IIdentityMessageService
    {
        private IEmailService _emailService;
        private IUserRepository _userRepository;
        private IConfirmUserMessageBuilder _confirmUserMessageBuilder;
        [Inject]
        public EmailServiceProvider(IEmailService emailService,
            IUserRepository userRepository,
            IConfirmUserMessageBuilder confirmUserMessageBuilder)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _confirmUserMessageBuilder = confirmUserMessageBuilder;
        }

        public Task SendAsync(IdentityMessage message)
        {
            User user = _userRepository.GetByEmail(message.Destination);
            _confirmUserMessageBuilder.Build(user, message.Body);
            return Task.Run(() =>
                {
                    _emailService.SendTextAsync(_confirmUserMessageBuilder);
                });
        }


    }
}
