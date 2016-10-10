using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.WebApi.Models.Response.Aggregates
{
    public class IdentityRoleResponseModel : ResponseModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}