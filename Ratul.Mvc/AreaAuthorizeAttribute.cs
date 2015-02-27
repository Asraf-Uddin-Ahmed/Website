using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ratul.Mvc
{
    public class AreaAuthorizeAttribute : AuthorizeAttribute
    {
        private UserIdentity _identity;
        private string _url;
        private string _typeOfUser;
        public AreaAuthorizeAttribute(string url, string typeOfUser)
        {
            _url = url;
            _typeOfUser = typeOfUser;
            _identity = (UserIdentity)HttpContext.Current.Session["CurrentUser"];
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (_identity != null)
            {
                // User valid
                if (_identity.TypeOfUser != _typeOfUser.ToString())
                {
                    throw new HttpException(502, "User is not authorized to access.");
                }

                // User exist
                if (_identity.ID != Guid.Empty)
                {
                    return true;
                }
            }
            HttpContext.Current.Response.BufferOutput = true;
            HttpContext.Current.Response.Redirect(_url, true);
            HttpContext.Current.Response.Close();
            return false;
        }
    }
}