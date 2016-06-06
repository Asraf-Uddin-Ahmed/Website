using Ninject.Modules;
using Ninject.Web.Common;
using Ratul.Utility;
using Website.Foundation.Persistence;

namespace Website.Web.Codes
{
    public class NinjectWebModule : NinjectModule
    {
        public override void Load()
        {
            Bind<RegexUtility>().ToSelf();
            Bind<ApplicationDbContext>().ToSelf().InRequestScope();
        }
    }
}