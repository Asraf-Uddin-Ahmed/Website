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
using Website.Web.Codes;

namespace Website.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IUserRepository _ur;
        private ILogger _logger;
        [Inject]
        public HomeController(IUserRepository ur, ILogger logger)
            : base(logger)
        {
            _ur = ur;
            _logger = logger;
        }

        public ActionResult Index()
        {
            IMembershipService ms = NinjectWebCommon.GetConcreteInstance<IMembershipService>();
            List<IUser> users = _ur.GetAll().Cast<IUser>().ToList();
            IUser user = (IUser)_ur.Get(new Guid("dacd8d97-b4cf-e411-be77-00266c4ad03f"));
            IUser user1 = (IUser)_ur.Get(new Guid("975ce24c-a6cf-e411-be77-00266c4ad03f"));

            return View();
        }

    }
}