using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Website.Web.Startup))]
namespace Website.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
