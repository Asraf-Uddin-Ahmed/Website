using log4net;
using Ninject;
using Ninject.Extensions.Logging;
using Ratul.Mvc;
using Ratul.Utility;
using Ratul.Utility.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Container;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Repositories;
using Website.Web.App_Start;
using Website.Web.Codes;
using Website.Web.Codes.Service;

namespace Website.Web.Controllers
{
    public class HomeController : BaseController
    {
        private ILogger _logger;
        [Inject]
        public HomeController(ILogger logger)
            : base(logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            IMembershipService ms = NinjectWebCommon.GetConcreteInstance<IMembershipService>();
            return View();
        }

    }
}