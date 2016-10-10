namespace Website.Foundation.Migrations
{
    using Website.Foundation.Core.Aggregates;
    using Website.Foundation.Core.Aggregates.Identity;
    using Website.Foundation.Core.Constant;
    using Website.Foundation.Core.Enums;
    using Website.Foundation.Persistence;
    using Website.Foundation.Persistence.Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Ratul.Utility;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Clients.Any())
            {
                PopulateClientsTable(context);
            }

            var roleManager = new RoleManager<CustomRole, Guid>(new RoleStore<CustomRole, Guid, CustomUserRole>(context));
            if (!roleManager.Roles.Any())
            {
                PopulateRolesTable(roleManager);
            }

            var manager = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, CustomRole, Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>(context));
            var user = new ApplicationUser()
            {
                Id = GuidUtility.GetNewSequentialGuid(),
                UserName = "ExamlogAdmin",
                Email = "13ratul@gmail.com",
                EmailConfirmed = true
            };
            manager.Create(user, "Examlog123Admin");
            var adminUser = manager.FindByName(user.UserName);
            manager.AddToRoles(adminUser.Id, new string[] { ApplicationRoles.ADMIN });

            if (!context.Settings.Any())
            {
                PopulateSetingsTable(context);
            }

            context.SaveChanges();
        }

        private static void PopulateRolesTable(RoleManager<CustomRole, Guid> roleManager)
        {
            roleManager.Create(new CustomRole { Id = GuidUtility.GetNewSequentialGuid(), Name = ApplicationRoles.ADMIN });
            roleManager.Create(new CustomRole { Id = GuidUtility.GetNewSequentialGuid(), Name = ApplicationRoles.SUPER_ADMIN });
            roleManager.Create(new CustomRole { Id = GuidUtility.GetNewSequentialGuid(), Name = ApplicationRoles.USER });
        }

        private static void PopulateClientsTable(ApplicationDbContext context)
        {
            List<Client> clientList = new List<Client> 
            {
                new Client
                { 
                    Id = "ngAuthApp", 
                    Secret= HashGenerator.GetHash("abc@123"), 
                    Name="AngularJS front-end Application", 
                    ApplicationType =  ApplicationType.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "*"
                },
                new Client
                { 
                    Id = "consoleApp", 
                    Secret=HashGenerator.GetHash("123@abc"), 
                    Name="Console Application", 
                    ApplicationType =ApplicationType.NativeConfidential, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, 
                    AllowedOrigin = "*"
                }
            };
            context.Clients.AddRange(clientList);
        }

        private static void PopulateSetingsTable(ApplicationDbContext context)
        {
            List<Settings> listSettings = new List<Settings>
            {
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Max Password Mistake", Name = SettingsName.MaxPasswordMistake.ToString(), Type = SettingsType.Integer, Value = "5"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email Host", Name = SettingsName.EmailHost.ToString(), Type = SettingsType.String, Value = "smtp.gmail.com"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email User Name", Name = SettingsName.EmailUserName.ToString(), Type = SettingsType.String, Value = "ratulprojectinfo@gmail.com"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email Password", Name = SettingsName.EmailPassword.ToString(), Type = SettingsType.String, Value = "projectinfo"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email Port", Name = SettingsName.EmailPort.ToString(), Type = SettingsType.Integer, Value = "587"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email Enable SSL", Name = SettingsName.EmailEnableSSL.ToString(), Type = SettingsType.Boolean, Value = "true"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "System Email Address", Name = SettingsName.SystemEmailAddress.ToString(), Type = SettingsType.String, Value = "info@examlog.com"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "System Email Name", Name = SettingsName.SystemEmailName.ToString(), Type = SettingsType.String, Value = "Examlog"}
            };
            listSettings.ForEach(s => context.Settings.AddOrUpdate(p => p.ID, s));
        }
    }
}
