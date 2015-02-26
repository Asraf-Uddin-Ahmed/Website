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
            Guid UserID = Guid.NewGuid();
            UserRepository ur = new UserRepository();
            ur.Add(new User()
                {
                    CreationTime = DateTime.UtcNow,
                    EmailAddress = "test@test.com",
                    ID = UserID,
                    LastLogin = null,
                    LastWrongPasswordAttempt = null,
                    Password = "test",
                    Status = UserStatus.Active,
                    TypeOfUser = UserType.Admin,
                    UpdateTime = DateTime.UtcNow,
                    UserName = "test",
                    WrongPasswordAttempt = 0
                });
            ICollection<User> l = ur.GetAll();
            UserVerificationRepository uvr = new UserVerificationRepository();
            uvr.Add(new UserVerification()
                {
                    CreationTime = DateTime.UtcNow,
                    ID = Guid.NewGuid(),
                    UserID = UserID,
                    VerificationCode = Guid.NewGuid().ToString()
                });
            return View();
        }

    }
}