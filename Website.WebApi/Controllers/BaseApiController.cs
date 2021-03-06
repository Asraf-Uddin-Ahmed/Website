﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Website.WebApi.Models;

namespace Website.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        private ILogger _logger;
        public BaseApiController(ILogger logger)
        {
            _logger = logger;
        }

        protected ExceptionResult InternalServerError(Exception ex, string message)
        {
            _logger.Error(ex, message);
            return base.InternalServerError(ex);
        }

        protected string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();

            if (queryStrings == null) return null;

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;

            return match.Value;
        }
        
    }
}
