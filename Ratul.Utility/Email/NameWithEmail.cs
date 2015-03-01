using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratul.Utility.Email
{
    public class NameWithEmail
    {
        internal string Name { get; set; }
        internal string EmailAddress { get; set; }
        public NameWithEmail(string name, string emailAddress)
        {
            Name = name;
            EmailAddress = emailAddress;
        }
    }
}
