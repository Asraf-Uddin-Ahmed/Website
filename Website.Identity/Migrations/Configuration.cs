namespace Website.Identity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Website.Identity.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<WebsiteIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebsiteIdentityDbContext context)
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var user = new ApplicationUser()
            //{
            //    UserName = "SuperPowerUser",
            //    Email = "13ratul@gmail.com",
            //    EmailConfirmed = true
            //};
            //manager.Create(user, "MySuperP@ssword!");
            //if (roleManager.Roles.Count() == 0)
            //{
            //    roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}
            //var adminUser = manager.FindByName("SuperPowerUser");
            //manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });

        }
    }
}
