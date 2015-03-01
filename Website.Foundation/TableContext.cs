using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Website.Foundation.Aggregates;

namespace Website.Foundation
{
    public class TableContext : DbContext, ITableContext
    {
        public TableContext()
            : base("name=DbConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserVerification> UserVerifications { get; set; }
        public DbSet<Settings> Settings { get; set; }

    }
}