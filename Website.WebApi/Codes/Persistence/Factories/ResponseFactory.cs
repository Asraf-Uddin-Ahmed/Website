using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Website.Foundation.Core.SearchData;
using Website.WebApi.Models;
using Website.WebApi.Models.Response;

namespace Website.WebApi.Codes.Persistence.Factories
{
    public abstract class ResponseFactory<TModel>
        where TModel : ResponseModel
    {
        protected UrlHelper UrlHelper { get; private set; }
        public ResponseFactory(HttpRequestMessage httpRequestMessage)
        {
            this.UrlHelper = new UrlHelper(httpRequestMessage);
        }

        protected ResponseCollectionModel<TModel> Create(IEnumerable<TModel> items, Pagination pagination, SortBy sortBy, int totalItem)
        {
            ResponseCollectionModel<TModel> responseModel = new ResponseCollectionModel<TModel>();
            responseModel.Items = items;
            responseModel.Pagination = pagination;
            responseModel.SortBy = sortBy;
            responseModel.TotalItem = totalItem;
            return responseModel;
        }
    }
}