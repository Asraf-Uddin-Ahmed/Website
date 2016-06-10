using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Website.WebApi.Dto.Response
{
    public abstract class ResponseDto
    {
        public string Href { get; set; }

        protected UrlHelper UrlHelper;



        public ResponseDto()
        {
        }

    }
}