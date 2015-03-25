﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Website.Web.Models;
using Website.Web.Models.Account;
using Website.Web.Codes;
using Website.Foundation.Enums;
using Ratul.Mvc;
using Ninject.Extensions.Logging;
using Website.Foundation.Container;
using Website.Foundation.Aggregates;

namespace Website.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ILogger _logger;
        private IMembershipService _membershipService;
        public AccountController(ILogger logger,
            IMembershipService membershipService)
            : base(logger)
        {
            _logger = logger;
            _membershipService = membershipService;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            LoginStatus loginStatus = _membershipService.ProcessLogin(model.UserName, model.Password);
            if(loginStatus == LoginStatus.Successful)
                return RedirectToAction("Index", "Home");
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                IUser user = model.CreateUser();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Invalid Email", ex.Message);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "User failed to create: UserName={0}, Email={1}", model.Email, model.Email);
            }
            return View(model);
        }

        //
        // GET: /Account/ConfirmUser
        [AllowAnonymous]
        public ActionResult ConfirmUser(string code)
        {
            try
            {
                VerificationStatus status = _membershipService.VerifyForUserStatus(code);
                if(status == VerificationStatus.Success)
                    ModelState.Add("Verification Success", new ModelState());
                else
                    ModelState.AddModelError("Verification Failed", "Verification Failed");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "User failed to verify: VerificationCode={0}", code);
                ModelState.AddModelError("Verification Failed", "Verification Failed");
            }
            return RedirectToAction("Register");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ChangePassword
        [AllowAnonymous]
        public ActionResult ChangePassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("ChangePasswordConfirmation");
        }

        //
        // GET: /Account/ChangePasswordConfirmation
        [AllowAnonymous]
        public ActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/Logout
        [AllowAnonymous]
        public ActionResult Logout()
        {
            UserSession.Clear();
            return RedirectToAction("Login");
        }
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}