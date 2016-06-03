using System;
using System.Collections.Generic;
using Ninject.Modules;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System.Web.Hosting;
using System.Configuration;
using Ratul.Utility;
using Website.Web.Codes.Persistence.Services;
using Website.Web.Codes.Core.Services;
using Website.Foundation.Persistence;
using Ninject.Web.Common;

namespace Website.Web.Codes
{
    public class NinjectWebModule : NinjectModule
    {
        public override void Load()
        {
            /*
             * MISCELLANEOUS
             * */
            Bind<RegexUtility>().ToSelf();
            Bind<TableContext>().ToSelf().InRequestScope();

            /*
             * SERVICE
             * */
            Bind<IValidationMessageService>().To<ValidationMessageService>();
            Bind<IUrlMakerService>().To<UrlMakerService>();
            
        }
    }
}