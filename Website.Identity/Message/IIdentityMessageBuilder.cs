using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Services.Email;
using Website.Identity.Models;

namespace Website.Identity.Message
{
    public interface IIdentityMessageBuilder : IMessageBuilder
    {
        void Build(ApplicationUser user, string subject, string body);
    }
}
