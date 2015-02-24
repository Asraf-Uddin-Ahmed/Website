using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Enums;

namespace Website.Foundation.Aggregates
{
    public class User : Entity, IUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public UserType TypeOfUser { get; set; }

        [Required]
        public UserStatus Status { get; set; }
        
        public DateTime? LastLogin { get; set; }

        [Required]
        public int WrongPasswordAttempt { get; set; }

        public DateTime? LastWrongPasswordAttempt { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public DateTime UpdateTime { get; set; }


        public ICollection<UserVerification> UserVerifications { get; set; }
    }
}
