using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.SearchData;

namespace Website.WebApi.Models.Response
{
    public class ResponseCollectionModel<TEntity, TModel> 
        where TEntity : Entity
        where TModel : ResponseModel
    {
        public IEnumerable<TModel> Items { get; set; }
        public Pagination Pagination { get; set; }
        public SortBy<TEntity> SortBy { get; set; }


        public ResponseCollectionModel()
        {
            this.Pagination = new Pagination();
            this.SortBy = new SortBy<TEntity>();
        }
    }
}