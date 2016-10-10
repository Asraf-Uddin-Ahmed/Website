using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Core.Aggregates.Identity
{
    public class CustomUserClaim : IdentityUserClaim<Guid> { }

}
