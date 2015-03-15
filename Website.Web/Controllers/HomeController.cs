using Ninject;
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
        IUserRepository _ur;
        IUserVerificationRepository _uvr;
        ISettingsRepository _sr;
        [Inject]
        public HomeController(IUserRepository ur, IUserVerificationRepository uvr, ISettingsRepository sr)
        {
            _ur = ur;
            _uvr = uvr;
            _sr = sr;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}