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
using Website.Foundation.Aggregates;
using Website.Foundation.Container;
using Website.Foundation.Enums;
using Website.Foundation.Repositories;
using Website.Web.App_Start;

namespace Website.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IUserRepository _ur;
        private ILogger _logger;
        [Inject]
        public HomeController(IUserRepository ur, ILogger logger) : base(logger)
        {
            _ur = ur;
            _logger = logger;
        }

        public ActionResult Index()
        {
            throw new Exception();
            //_logger.Error("Custom error message -> " + DateTime.Now);
            //_logger.Info("Custom info message -> " + DateTime.Now);
            //_logger.Fatal("Custom info message -> " + DateTime.Now);
            return View();
        }

        
        
    }
}