using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.SearchData;

namespace Website.Foundation.Core.Repositories
{
    public interface IRepositorySearch<TEntity, TSearch> : IRepository<TEntity>
        where TEntity : Entity
        where TSearch : EntitySearch
    {
        ICollection<TEntity> GetByAnd(TSearch searchItem, Pagination pagination, OrderBy<TEntity> orderBy);
        ICollection<TEntity> GetByOr(TSearch searchItem, Pagination pagination, OrderBy<TEntity> orderBy);
        int GetTotalAnd(TSearch searchItem);
        int GetTotalOr(TSearch searchItem);
    }
}
