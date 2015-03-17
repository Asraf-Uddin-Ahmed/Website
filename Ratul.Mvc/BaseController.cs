using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ratul.Mvc
{
    public class BaseController : Controller
    {
        private ILogger _logger;
        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            //filterContext.Result = new ViewResult
            //{
            //    ViewName = "~/Views/Shared/Error.aspx"
            //};
            _logger.Fatal(filterContext.Exception, "Fatal Error Occurred");
            filterContext.ExceptionHandled = true;
        }
    }
}
