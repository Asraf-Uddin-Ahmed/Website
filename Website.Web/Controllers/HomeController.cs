using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Foundation.Aggregates;
using Website.Foundation.Enums;
using Website.Foundation.Repositories;

namespace Website.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //UserRepository ur = new UserRepository();
            //ur.Add(new User()
            //    {
            //        CreationTime = DateTime.UtcNow,
            //        EmailAddress = "test@test.com",
            //        ID = Guid.NewGuid(),
            //        LastLogin = null,
            //        LastWrongPasswordAttempt = null,
            //        Password = "test",
            //        Status = UserStatus.Active,
            //        TypeOfUser = UserType.Admin,
            //        UpdateTime = DateTime.UtcNow,
            //        UserName = "test",
            //        WrongPasswordAttempt = 0
            //    });
            //ICollection<User> l = ur.GetAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}