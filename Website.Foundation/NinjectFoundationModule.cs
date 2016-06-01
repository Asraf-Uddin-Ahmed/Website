using System;
using System.Collections.Generic;
using Ninject.Modules;
using System.Web;
using System.Configuration;
using Website.Foundation.Persistence;
using Website.Foundation.Core.Factories;
using Website.Foundation.Factories;
using Website.Foundation.Core.Services;
using Website.Foundation.Persistence.Services;
using Website.Foundation.Core;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Persistence.Repositories;

namespace Website.Foundation
{
    public class NinjectFoundationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<TableContext>().ToSelf();

            /*
             * REPOSITORY
             * */
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserVerificationRepository>().To<UserVerificationRepository>();
            Bind<IPasswordVerificationRepository>().To<PasswordVerificationRepository>();
            Bind<ISettingsRepository>().To<SettingsRepository>();

            /*
             * FACTORY
             * */
            Bind<IUserFactory>().To<UserFactory>();

            /*
             * SERVICE
             * */
            Bind<IUserService>().To<UserService>();

        }
    }
}