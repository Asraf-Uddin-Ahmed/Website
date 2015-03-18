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
    [AreaAuthorize("/Account/Login", "Admin")]
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
            return View();
        }
        
    }
}