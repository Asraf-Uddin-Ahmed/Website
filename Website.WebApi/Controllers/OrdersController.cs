using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.SearchData;
using Website.Foundation.Core.Services;
using Website.WebApi.Codes.Core.Factories;
using Website.WebApi.Codes.Core.Identity;
using Website.WebApi.Models.Request;

namespace Website.WebApi.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private IUserService _userSevice;
        private IUserResponseFactory _userResponseFactory;
        public OrdersController(IUserService userSevice, IUserResponseFactory userResponseFactory)
        {
            _userSevice = userSevice;
            _userResponseFactory = userResponseFactory;
        }

        [Authorize(Roles = "IncidentResolvers")]
        [HttpPut]
        [Route("refund/{orderId}")]
        public IHttpActionResult RefundOrder([FromUri]string orderId)
        {
            return Ok();
        }

        [ClaimsAuthorization(ClaimType = "FTE", ClaimValue = "1")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("users")]
        public IHttpActionResult GetMyUsers([FromUri] RequestSearchModel<User, UserSearch> searchModel)
        {
            return Ok(_userResponseFactory.Create(
                _userSevice.GetUserBy(searchModel.Pagination, searchModel.OrderBy), 
                searchModel.Pagination,
                searchModel.SortBy,
                _userSevice.GetTotal()));
        }

        [Route("globallog")]
        [HttpGet]
        public void TestGlobalLog()
        {
            int I = 0;
            int J = 10 / I;
        }
    }
}
