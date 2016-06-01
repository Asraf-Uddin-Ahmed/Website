using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services;

namespace Website.Foundation.Persistence.Services
{
    public class PasswordVerificationService : IPasswordVerificationService
    {
        private ILogger _logger;
        private IPasswordVerificationRepository _passwordVerificationRepository;
        [Inject]
        public PasswordVerificationService(ILogger logger,
            IPasswordVerificationRepository passwordVerificationRepository)
        {
            _logger = logger;
            _passwordVerificationRepository = passwordVerificationRepository;
        }

        public void RemoveByUserID(Guid userID)
        {
            _passwordVerificationRepository.RemoveByUserID(userID);
            _passwordVerificationRepository.Commit();
        }
    }
}
