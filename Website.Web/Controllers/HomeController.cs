using log4net;
using Ninject;
using Ninject.Extensions.Logging;
using Ratul.Utility;
using Ratul.Utility.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Foundation.Aggregates;
using Website.Foundation.Container;
using Website.Foundation.Enums;
using Website.Foundation.Repositories;
using Website.Web.App_Start;

namespace Website.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _ur;
        private IUserVerificationRepository _uvr;
        private ISettingsRepository _sr;
        private ILogger _logger;
        [Inject]
        public HomeController(IUserRepository ur, IUserVerificationRepository uvr, ISettingsRepository sr, ILogger logger)
        {
            _ur = ur;
            _uvr = uvr;
            _sr = sr;
            _logger = logger;
        }

        public ActionResult Index()
        {
            _logger.Error("Custom error message -> " + DateTime.Now);
            return View();
        }

    }
}