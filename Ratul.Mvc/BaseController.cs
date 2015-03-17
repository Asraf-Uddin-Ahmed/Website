using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            _logger.Fatal(filterContext.Exception, "Fatal Error Occurred");
            throw new HttpException((int)System.Net.HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}
