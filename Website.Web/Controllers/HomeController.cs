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
            List<IUser> list = _ur.GetAllPaged(1, 2, (c) => c.Password).Cast<IUser>().ToList();
            int total = _ur.GetTotal();

            List<IUser> listAnd0 = _ur.GetPagedAnd(new UserSearch(), 2, 2, (c) => c.Password).ToList();
            List<IUser> listAnd1 = _ur.GetPagedAnd(new UserSearch() { UserName = "test" }, 1, 2, (c) => c.Password).ToList();
            List<IUser> listAnd2 = _ur.GetPagedAnd(new UserSearch() { UserName = "test", WrongPasswordAttempt = 2 }, 1, 2, (c) => c.Password).ToList();
            List<IUser> listAnd3 = _ur.GetPagedAnd(new UserSearch() { UserName = "test", Status = UserStatus.Blocked }, 1, 2, (c) => c.Password).ToList();

            int totalOr1 = _ur.GetTotalOr(new UserSearch() { UserName = "rat" });
            int totalOr2 = _ur.GetTotalOr(new UserSearch() { UserName = "rat", WrongPasswordAttempt = 2 });
            int totalOr3 = _ur.GetTotalOr(new UserSearch());
            int totalOr4 = _ur.GetTotalOr(new UserSearch() { UserName = "rat", Status = UserStatus.Blocked });
            int totalOr5 = _ur.GetTotalOr(new UserSearch() { UserName = "rat", Status = UserStatus.Blocked, EmailAddress = "test2@test.com" });

            int totalAnd1 = _ur.GetTotalAnd(new UserSearch() { UserName = "test" });
            int totalAnd2 = _ur.GetTotalAnd(new UserSearch() { UserName = "test", WrongPasswordAttempt = 2 });
            int totalAnd3 = _ur.GetTotalAnd(new UserSearch());
            int totalAnd4 = _ur.GetTotalAnd(new UserSearch() { UserName = "test", Status = UserStatus.Blocked });
            return View();
        }

    }
}