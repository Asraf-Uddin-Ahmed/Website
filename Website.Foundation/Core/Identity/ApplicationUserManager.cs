using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services.Email;
using Website.Foundation.Persistence;
using Website.Foundation.Persistence.Repositories;
using Website.Foundation.Persistence.Services.Email;

namespace Website.Foundation.Core.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            // TODO: Ninject
            var appDbContext = context.Get<ApplicationDbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

            /*Configure validation logic for usernames*/
            //appUserManager.UserValidator = new UserValidator<ApplicationUser>(appUserManager)
            //{
            //    AllowOnlyAlphanumericUserNames = true,
            //    RequireUniqueEmail = true
            //};
            //appUserManager.UserValidator = new MyCustomUserValidator(appUserManager);

            /*Configure validation logic for passwords*/
            //appUserManager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = false,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};
            //appUserManager.PasswordValidator = new MyCustomPasswordValidator();

            /*Configure email confirmation settings*/
            // TODO: Ninject
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            IUserRepository userRepository = new UserRepository(applicationDbContext);
            ISettingsRepository settingsRepository = new SettingsRepository(applicationDbContext);
            IConfirmUserMessageBuilder confirmUserMessageBuilder = new ConfirmUserMessageBuilder(settingsRepository);
            IEmailService emailService = new EmailService(settingsRepository);
            appUserManager.EmailService = new EmailServiceProvider(emailService, userRepository, confirmUserMessageBuilder);

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return appUserManager;
        }
    }
}