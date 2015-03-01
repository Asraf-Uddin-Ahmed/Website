using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Website.Foundation.Repositories
{
    public class UserVerificationRepository : BaseEfRepository<UserVerification>, IUserVerificationRepository
    {
        private TableContext _context;
        [Inject]
        public UserVerificationRepository(TableContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
