using System;
using System.Collections.Generic;
using Ninject.Modules;
using System.Web;
using System.Configuration;
using Website.Foundation.Aggregates;
using Website.Foundation.Repositories;

namespace Website.Foundation
{
    public class NinjectFoundationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITableContext>().To<TableContext>();

            // ENTITY
            Bind<IUser>().To<User>();
            Bind<IUserVerification>().To<UserVerification>();
            Bind<ISettings>().To<Settings>();

            // REPOSITORY
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserVerificationRepository>().To<UserVerificationRepository>();
            Bind<ISettingsRepository>().To<SettingsRepository>();
        }
    }
}