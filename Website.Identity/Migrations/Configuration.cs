namespace Website.Identity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Website.Identity.Aggregates;
    using Website.Identity.Enums;
    using Website.Identity.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<AuthDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuthDbContext context)
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

            if (context.Clients.Count() == 0)
            {
                context.Clients.AddRange(BuildClientsList());
                context.SaveChanges();
            }
        }

        private static List<Client> BuildClientsList()
        {
            List<Client> ClientsList = new List<Client> 
            {
                new Client
                { 
                    Id = "ngAuthApp", 
                    Secret= HashGenerator.GetHash("abc@123"), 
                    Name="AngularJS front-end Application", 
                    ApplicationType =  ApplicationTypes.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "http://ngauthenticationweb.azurewebsites.net"
                },
                new Client
                { 
                    Id = "consoleApp", 
                    Secret=HashGenerator.GetHash("123@abc"), 
                    Name="Console Application", 
                    ApplicationType =ApplicationTypes.NativeConfidential, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, 
                    AllowedOrigin = "*"
                }
            };
            return ClientsList;
        }
    }
}
