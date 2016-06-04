using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Services;
using Website.Foundation.Core.Services.Email;
using Website.Web.App_Start;
using Website.Web.Codes.Core.Services;

namespace Website.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public void ProcessForgotPassword()
        {
            IMembershipService membershipService = NinjectWebCommon.GetConcreteInstance<IMembershipService>();
            IUserService userService = NinjectWebCommon.GetConcreteInstance<IUserService>();
            IUrlMakerService urlMakerHelper = NinjectWebCommon.GetConcreteInstance<IUrlMakerService>();
            IEmailService emailService = NinjectWebCommon.GetConcreteInstance<IEmailService>();
            IForgotPasswordMessageBuilder forgotPasswordMessageBuilder = NinjectWebCommon.GetConcreteInstance<IForgotPasswordMessageBuilder>();

            User user = userService.GetUserByEmail(this.Email);
            PasswordVerification passwordVerification = membershipService.ProcessForgotPassword(user);
            string url = urlMakerHelper.GetUrlForgotPassword(passwordVerification.VerificationCode);
            forgotPasswordMessageBuilder.Build(user, url);
            emailService.SendText(forgotPasswordMessageBuilder);
        }
    }
}