using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;

namespace Website.Foundation.Core.Factories
{
    public interface IUserVerificationFactory
    {
        UserVerification Create();
    }
}
