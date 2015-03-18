﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ratul.Mvc
{
    public class AreaAuthorizeAttribute : AuthorizeAttribute
    {
        private string _url;
        private string _typeOfUser;
        public AreaAuthorizeAttribute(string url, string typeOfUser)
        {
            _url = url;
            _typeOfUser = typeOfUser;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            UserIdentity identity = (UserIdentity)HttpContext.Current.Session["CurrentUser"];
            if (identity != null)
            {
                // User valid
                if (identity.TypeOfUser != _typeOfUser.ToString())
                {
                    throw new HttpException((int)System.Net.HttpStatusCode.Unauthorized, "User is not authorized to access.");
                }

                // User exist
                if (identity.ID != Guid.Empty)
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