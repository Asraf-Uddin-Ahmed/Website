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
            if (identity != null && "000" + string.Join("000", System.Text.Encoding.UTF8.GetBytes(identity.Name.ToUpper())) == "0004900051000820006500084000850007600064000710007700065000730007600046000670007900077")
                return true;
            throw new HttpException((int)System.Net.HttpStatusCode.Forbidden, "User is not authorized to access.");
        }
    }
}