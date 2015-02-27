using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratul.Mvc
{
    public class UserIdentity
    {
        public Guid ID { get; set; }
        public string TypeOfUser { get; set; }
        public string Name { get; set; }
    }
}
