using Ratul.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Web.Codes
{
    public static class UserSession
    {
        private enum SessionKeys
        {
            CurrentUser,
            UserTimeZoneOffsetInMinute,
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public static UserIdentity CurrentUser
        {
            get
            {
                UserIdentity identity = (UserIdentity)HttpContext.Current.Session[SessionKeys.CurrentUser.ToString()];
                if (identity == null && HttpContext.Current.Request.IsAuthenticated)
                {
                    identity = (UserIdentity)HttpContext.Current.Session[SessionKeys.CurrentUser.ToString()];
                }
                return identity;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.CurrentUser.ToString()] = value;
            }
        }

        public static int UserTimeZoneOffsetInMinute
        {
            get
            {
                return (int)HttpContext.Current.Session[SessionKeys.UserTimeZoneOffsetInMinute.ToString()];
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.UserTimeZoneOffsetInMinute.ToString()] = value;
            }
        }
    }
}