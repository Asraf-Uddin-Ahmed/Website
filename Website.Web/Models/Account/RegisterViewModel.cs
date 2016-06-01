using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Container;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Factories;
using Website.Foundation.Core.Services;
using Website.Web.App_Start;
using Website.Web.Codes;
using Website.Web.Codes.Helper;
using Website.Web.Codes.Service;

namespace Website.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public User CreateUser()
        {
            IMembershipService membershipService = NinjectWebCommon.GetConcreteInstance<IMembershipService>();
            IUserFactory userFactory = NinjectWebCommon.GetConcreteInstance<IUserFactory>();
            User user = userFactory.Create(this.Password);
            user.EmailAddress = this.Email;
            user.UserName = this.Email;
            user.Name = this.Email;
            user.Status = UserStatus.Unverified;
            user.TypeOfUser = UserType.Employee;
            user = membershipService.CreateUser(user);
            return user;
        }
        public void SendCofirmEmailIfRequired(User user)
        {
            if(!user.UserVerifications.Any())
                return;
            IUrlMakerHelper urlMakerHelper = NinjectWebCommon.GetConcreteInstance<IUrlMakerHelper>();
            IEmailService emailService = NinjectWebCommon.GetConcreteInstance<IEmailService>();
            string url = urlMakerHelper.GetUrlConfirmUser(user.UserVerifications.First().VerificationCode);
            emailService.SendConfirmUser(user, url);
        }
    }
}