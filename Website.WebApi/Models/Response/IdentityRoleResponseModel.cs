using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.WebApi.Models.Response
{
    public class IdentityRoleResponseModel : ResponseModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}