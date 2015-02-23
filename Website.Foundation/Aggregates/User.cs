using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Aggregates
{
    public class User : Website.Foundation.Aggregates.IUser
    {
        [Required]
        public Guid ID { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public DateTime UpdateTime { get; set; }
    }
}
