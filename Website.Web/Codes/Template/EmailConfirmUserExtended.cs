using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Foundation.Core.Aggregates;

namespace Website.Web.Codes.Template
{
    public partial class EmailConfirmUser
    {
        public User NewUser
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public EmailConfirmUser(User newUser, string url)
        {
            NewUser = newUser;
            Url = url;
        }
    }
}