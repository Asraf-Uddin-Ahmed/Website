using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratul.Utility.Email
{
    public class NameWithEmail
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public NameWithEmail(string name, string emailAddress)
        {
            Name = name;
            EmailAddress = emailAddress;
        }
    }
}
