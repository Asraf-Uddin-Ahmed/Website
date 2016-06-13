using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;

namespace Website.Foundation.Core.SearchData
{
    public class SortBy<TEntity> where TEntity : Entity
    {
        private static readonly Expression<Func<TEntity, dynamic>> DEFAULT_PREDICATE_ORDERBY = (col) => col.ID;


        public Expression<Func<TEntity, dynamic>> PredicateOrderBy { get; private set; }
        public bool IsAscending { get; private set; }


        public SortBy(Expression<Func<TEntity, dynamic>> predicateOrderBy, bool isAscending)
        {
            this.PredicateOrderBy = predicateOrderBy;
            this.IsAscending = IsAscending;
        }
        public SortBy() : this(DEFAULT_PREDICATE_ORDERBY, true)
        {
        }
    }
}
