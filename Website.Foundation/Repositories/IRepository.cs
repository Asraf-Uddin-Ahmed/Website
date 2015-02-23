using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Repositories
{
    public interface IRepository<Type>
    {
        void Add(Type item);
        void Update(Type item);
        void Remove(int id);
        Type Get(int id);
        ICollection<Type> GetAll();
    }
}
