using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Aggregates;

namespace Website.Foundation.Repositories
{
    public interface IBaseEfRepository<TEntity>
    {
        void Add(IEntity entity);
        void Update(IEntity entity);
        void Remove(Guid ID);
        void Remove(IEntity currentItem);
        IEntity Get(Guid ID);
        ICollection<IEntity> GetAll();
        int GetTotal();
        ICollection<IEntity> GetAllPaged(int pageNumber, int pageSize);
    }
}
