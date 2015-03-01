using Ninject;
using Ratul.Utility;
using Ratul.Utility.Email;
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
            //int total = _ur.GetTotal();
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

            //Guid salt = GuidUtility.GetNewSequentialGuid();
            //string original = "ratul";
            //string encrpyted = CryptographicUtility.Encrypt(original, salt);
            //string decrpyted = CryptographicUtility.Decrypt(encrpyted, salt);

            //EmailSettings emailSettings = new EmailSettings();
            //emailSettings.EnableSsl = true;
            //emailSettings.Host = "smtp.gmail.com";
            //emailSettings.Port = 587;
            //emailSettings.UserName = "ratul@proggasoft.com";
            //emailSettings.Password = "progg@soft";

            //MessageSettings messageSettings = new MessageSettings();
            //messageSettings.Subject = "Test Utility Email";
            //messageSettings.Body = string.Format("hello {0}, how are you.\noriginal is {1}\nencrypted: {2}\ndecrypted: {3}", salt, original, encrpyted, decrpyted);
            //messageSettings.IsBodyHtml = false;
            //messageSettings.ToList = new List<NameWithEmail>() { new NameWithEmail("ratul_gmail", "13ratul@gmail.com"), new NameWithEmail("ratul_yahoo", "ratul840@yahoo.com") };
            //messageSettings.From = new NameWithEmail("ratul_progga", "ratul@proggasoft.com");
            //messageSettings.ReplyToList = new List<NameWithEmail>() { new NameWithEmail("noreply_1", "noreply1@proggasoft.com"), new NameWithEmail("noreply_2", "noreply2@proggasoft.com") };

            //EmailSender es = new EmailSender(emailSettings);
            //try
            //{
            //    bool isSend = es.Send(messageSettings);
            //}
            //catch (Exception ex)
            //{
            //}
            
            return View();
        }

    }
}