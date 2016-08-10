using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services;

namespace Website.Foundation.Persistence.Services
{
    public class PasswordVerificationService : IPasswordVerificationService
    {
        private ILogger _logger;
        private IUnitOfWork _unitOfWork;
        [Inject]
        public PasswordVerificationService(ILogger logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void RemoveByUserID(Guid userID)
        {
            _unitOfWork.PasswordVerifications.RemoveByUserID(userID);
            _unitOfWork.Commit();
        }
    }
}
