using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.SearchData;

namespace Website.Foundation.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity currentItem);
        void Remove(Guid ID);
        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Get(Guid ID);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetBy(Pagination pagination, OrderBy<TEntity> sortBy);

        int GetTotal();
    }
}
