using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratul.Mvc.Authorization
{
    public class UserIdentity
    {
        public Guid ID { get; private set; }
        public string TypeOfUser { get; private set; }
        public string Name { get; private set; }

        public UserIdentity(Guid id, string typeOfUser, string name)
        {
            ID = id;
            TypeOfUser = typeOfUser;
            Name = name;
        }
    }
}
