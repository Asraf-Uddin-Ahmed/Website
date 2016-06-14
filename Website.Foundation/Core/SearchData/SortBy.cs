using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;

namespace Website.Foundation.Core.SearchData
{
    public class SortBy<TEntity> where TEntity : Entity
    {
        private static readonly Func<TEntity, dynamic> DEFAULT_PREDICATE_ORDERBY = (col) => col.ID;


        public Func<TEntity, dynamic> PredicateOrderBy { get; set; }
        public bool IsAscending { get; set; }


        public SortBy(Func<TEntity, dynamic> predicateOrderBy, bool isAscending)
        {
            this.PredicateOrderBy = predicateOrderBy;
            this.IsAscending = isAscending;
        }
        public SortBy() : this(DEFAULT_PREDICATE_ORDERBY, true)
        {
        }
    }
}
