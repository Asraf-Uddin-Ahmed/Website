using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;

namespace Website.Foundation.Core.Services.Email
{
    public interface IConfirmUserMessageBuilder : IMessageBuilder
    {
        void Build(User newUser, string url);
    }
}
