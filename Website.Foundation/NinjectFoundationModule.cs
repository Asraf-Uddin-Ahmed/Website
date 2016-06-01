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

namespace Website.Foundation
{
    public class NinjectFoundationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<TableContext>().ToSelf();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            
            // FACTORY
            Bind<IUserFactory>().To<UserFactory>();

            // SERVICE
            Bind<IUserService>().To<UserService>();

        }
    }
}