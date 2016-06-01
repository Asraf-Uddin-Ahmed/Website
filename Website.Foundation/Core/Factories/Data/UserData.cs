using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Enums;

namespace Website.Foundation.Core.Factories.Data
{
    public class UserData
    {
        public string UserName {get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserType TypeOfUser { get; set; }
        public bool HasVerificationCode { get; set; }
    }
}
