using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Website.Identity.Model;

namespace Website.Identity
{
    public class WebsiteIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public WebsiteIdentityDbContext()
            : base("IdentityConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public static WebsiteIdentityDbContext Create()
        {
            return new WebsiteIdentityDbContext();
        }

    }
}