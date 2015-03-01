using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Website.Foundation.Repositories
{
    public class UserRepository : BaseEfRepository<User>, IUserRepository
    {
        private TableContext _context;
        [Inject]
        public UserRepository(TableContext context)
            : base(context)
        {
            _context = context;
        }

        public bool IsUserNameExist(string userName)
        {
            bool isExist = _context.Users.Any(col => col.UserName == userName);
            return isExist;
        }
        public bool IsEmailExist(string email)
        {
            bool isExist = _context.Users.Any(col => col.EmailAddress == email);
            return isExist;
        }
    }
}
