﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Persistence.EntityConfigurations;

namespace Website.Foundation.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> ExtendedUsers { get; set; }
        public DbSet<UserVerification> UserVerifications { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<PasswordVerification> PasswordVerifications { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}