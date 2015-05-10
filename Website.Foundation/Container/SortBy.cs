using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Aggregates;

namespace Website.Foundation.Container
{
    public class SortBy<TEntity> where TEntity : Entity, IEntity, new()
    {
        public Func<TEntity, dynamic> PredicateOrderBy { get; set; }
        public bool IsAscending { get; set; }
    }
}
