using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Web.Codes.Core.Services;
using Website.Web.Codes.Core.Services.UriMaker;

namespace Website.Web.Codes.Persistence.Services.UriMaker
{
    public class UriMakerService : IUriMakerService
    {
        private static string _siteUrl;
        public string SiteUrl
        {
            get
            {
                if(string.IsNullOrEmpty(_siteUrl))
                {
                    string authority = HttpContext.Current.Request.Url.Authority;
                    string scheme = HttpContext.Current.Request.Url.Scheme;
                    _siteUrl = scheme + "://" + authority;
                }
                return _siteUrl;
            }
        }




        public string GetFullUri(IUriBuilder uriBuilder)
        {
            return this.SiteUrl + "/" + uriBuilder.GetUri();
        }

        public string GetRelativeUri(IUriBuilder uriBuilder)
        {
            return uriBuilder.GetUri();
        }
    }
}