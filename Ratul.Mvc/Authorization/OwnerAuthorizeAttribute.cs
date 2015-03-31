using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ratul.Mvc.Authorization
{
    public class OwnerAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            UserIdentity identity = (UserIdentity)HttpContext.Current.Session["CurrentUser"];
            if (identity != null && identity.Name == "13ratul@gmail.com")
                return true;
            throw new HttpException((int)System.Net.HttpStatusCode.Forbidden, "User is not authorized to access.");
        }
    }
}