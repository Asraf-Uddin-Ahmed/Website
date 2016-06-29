using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Website.WebApi.Configuration;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using PartialResponse.Net.Http.Formatting;

namespace Website.WebApi.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            httpConfig.Formatters.Clear();
            PartialJsonMediaTypeFormatter partialJsonMediaTypeFormatter = new PartialJsonMediaTypeFormatter() { IgnoreCase = true };
            partialJsonMediaTypeFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpConfig.Formatters.Add(partialJsonMediaTypeFormatter);

            httpConfig.MapHttpAttributeRoutes();

            httpConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            httpConfig.EnableCors();

            httpConfig.Services.Add(typeof(IExceptionLogger), new UnhandledExceptionLogger());

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //app.UseWebApi(httpConfig);
            app.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);
            app.UseNinjectWebApi(httpConfig);
        }
    }
}