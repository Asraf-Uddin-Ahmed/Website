using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Website.WebApi.Codes.Persistence.Factories
{
    public abstract class ResponseFactory
    {
        protected UrlHelper UrlHelper { get; private set; }
        public ResponseFactory(HttpRequestMessage httpRequestMessage)
        {
            this.UrlHelper = new UrlHelper(httpRequestMessage);
        }
    }
}