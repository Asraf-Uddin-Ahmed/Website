using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Foundation.Aggregates;
using Website.Foundation.Enums;
using Website.Foundation.Repositories;
using Website.Web.App_Start;

namespace Website.Web.Controllers
{
    public class HomeController : Controller
    {
        //IUserRepository _ur;
        //IUserVerificationRepository _uvr;
        //ISettingsRepository _sr;
        //[Inject]
        //public HomeController(IUserRepository ur, IUserVerificationRepository uvr, ISettingsRepository sr)
        //{
        //    _ur = ur;
        //    _uvr = uvr;
        //    _sr = sr;
        //}

        public ActionResult Index()
        {
            //Guid UserID = Guid.NewGuid();
            //IUser user = NinjectWebCommon.GetConcreteInstance<IUser>();
            //user.CreationTime = DateTime.UtcNow;
            //user.EmailAddress = "test@test.com";
            //user.ID = UserID;
            //user.LastLogin = null;
            //user.LastWrongPasswordAttempt = null;
            //user.Password = "test";
            //user.Status = UserStatus.Active;
            //user.TypeOfUser = UserType.Admin;
            //user.UpdateTime = DateTime.UtcNow;
            //user.UserName = "test";
            //user.WrongPasswordAttempt = 0;
            //_ur.Add(user);
            //IUser u = (IUser)_ur.Get(user.ID);
            //ICollection<IUser> listU = _ur.GetAll().Cast<IUser>().ToList();
            //IUserVerificationRepository uvr = NinjectWebCommon.GetConcreteInstance<IUserVerificationRepository>();
            //uvr.Add(new UserVerification()
            //    {
            //        CreationTime = DateTime.UtcNow,
            //        ID = Guid.NewGuid(),
            //        UserID = UserID,
            //        VerificationCode = Guid.NewGuid().ToString()
            //    });
            //ISettingsRepository sr = NinjectWebCommon.GetConcreteInstance<ISettingsRepository>();
            //sr.Add(new Settings()
            //{
            //    DisplayName = "dis name",
            //    ID = Guid.NewGuid(),
            //    Name = "main name",
            //    Type = SettingsType.String,
            //    Value = "hello"
            //});
            return View();
        }

    }
}