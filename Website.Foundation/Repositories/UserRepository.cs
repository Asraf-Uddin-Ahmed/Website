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
            return _context.Confessions.ToList();
        }

        public User Get(int confessionID)
        {
            return _context.Confessions.Find(confessionID);   // .Where(row => row.ConfessionID == id).SingleOrDefault();
        }

        public void Add(User item)
        {
            _context.Confessions.Add(item);
            _context.SaveChanges();
        }

        public void Update(User item)
        {
            User currentItem = Get(item.ConfessionID);
            if (currentItem == null)
                return;
            _context.Entry(currentItem).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }

        public void Remove(int confessionID)
        {
            User currentItem = Get(confessionID);
            if (currentItem == null)
                return;
            _context.Confessions.Remove(currentItem);
            _context.SaveChanges();
        }
    }
}
