using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Identity;

namespace Website.WebApi.Dto.Response
{
    public class ApplicationUserResponseDto : ResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }

        public ApplicationUserResponseDto()
        {

        }
    }
}