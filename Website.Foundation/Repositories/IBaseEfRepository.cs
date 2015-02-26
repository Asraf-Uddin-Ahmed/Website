using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Repositories
{
    public interface IBaseEfRepository<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(Guid ID);
        TEntity Get(Guid ID);
        ICollection<TEntity> GetAll();
    }
}
