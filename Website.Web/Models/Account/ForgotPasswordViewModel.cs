using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Website.Foundation.Aggregates;
using Website.Foundation.Repositories;
using Website.Web.App_Start;
using Website.Web.Codes.Helper;
using Website.Web.Codes.Service;

namespace Website.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}