using System;
using System.Collections.Generic;
using Ninject.Modules;
using System.Web;
using System.Configuration;
using Website.Foundation.Aggregates;
using Website.Foundation.Repositories;
using Website.Foundation.Helpers;
using Website.Foundation.Factory;
using Website.Foundation.Services;

namespace Website.Foundation
{
    public class NinjectFoundationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITableContext>().To<TableContext>();

            // ENTITY
            Bind<IPasswordVerification>().To<PasswordVerification>();
            Bind<IUser>().To<User>();
            Bind<IUserVerification>().To<UserVerification>();
            Bind<ISettings>().To<Settings>();

            // REPOSITORY
            Bind<IPasswordVerificationRepository>().To<PasswordVerificationRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserVerificationRepository>().To<UserVerificationRepository>();
            Bind<ISettingsRepository>().To<SettingsRepository>();

            // FACTORY
            Bind<IUserFactory>().To<UserFactory>();

            // INTERNAL
            Bind<IRepositorySearchHelper>().To<RepositorySearchHelper>();

            // HELPER

            // SERVICE
            Bind<IUserService>().To<UserService>();

        }
    }
}