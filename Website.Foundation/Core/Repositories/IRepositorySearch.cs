using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Container;
using Website.Foundation.Core.SearchData;

namespace Website.Foundation.Core.Repositories
{
    public interface IRepositorySearch<TEntity, TSearch> : IRepository<TEntity>
        where TEntity : Entity
        where TSearch : EntitySearch
    {
        IEnumerable<TEntity> GetByAnd(TSearch searchItem, int index, int size, SortBy<TEntity> sortBy);
        IEnumerable<TEntity> GetByOr(TSearch searchItem, int index, int size, SortBy<TEntity> sortBy);
        int GetTotalAnd(TSearch searchItem);
        int GetTotalOr(TSearch searchItem);
    }
}
