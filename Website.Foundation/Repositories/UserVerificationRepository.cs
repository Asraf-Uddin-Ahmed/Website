using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Repositories
{
    public class UserVerificationRepository : BaseEfRepository<UserVerification>, IUserVerificationRepository
    {
        private TableContext _context;
        public UserVerificationRepository()
        {
            _context = new TableContext();
        }
    }
}
