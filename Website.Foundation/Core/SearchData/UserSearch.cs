using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Enums;

namespace Website.Foundation.Core.SearchData
{
    public class UserSearch : EntitySearch
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public UserType? TypeOfUser { get; set; }
        public UserStatus? Status { get; set; }
        public int? WrongPasswordAttempt { get; set; }
        public DateTime? LastWrongPasswordAttempt { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
