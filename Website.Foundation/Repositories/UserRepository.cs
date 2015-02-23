using Website.Foundation.Aggregates;
using Foundation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private TableContext _context;
        public UserRepository()
        {
            _context = new TableContext();
        }


        public ICollection<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Get(Guid ID)
        {
            return _context.Users.Find(ID);   // .Where(row => row.ConfessionID == id).SingleOrDefault();
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            User currentItem = Get(entity.ID);
            if (currentItem == null)
                return;
            _context.Entry(currentItem).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public void Remove(Guid ID)
        {
            User currentItem = Get(ID);
            if (currentItem == null)
                return;
            _context.Users.Remove(currentItem);
            _context.SaveChanges();
        }
    }
}
