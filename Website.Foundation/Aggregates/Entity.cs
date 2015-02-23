using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Aggregates
{
    public class Entity : IEntity
    {
        [Required]
        public Guid ID { get; set; }
    }
}
