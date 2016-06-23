using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Website.Identity.Repositories;

namespace Website.WebApi.Controllers.Identity
{
    [Authorize(Roles = "Admin")]
    public class RefreshTokensController : BaseApiController
    {

        private AuthRepository _authRepository;

        public RefreshTokensController()
        {
            _authRepository = new AuthRepository();
        }

        public IHttpActionResult Get()
        {
            return Ok(_authRepository.GetAllRefreshTokens());
        }

        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await _authRepository.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _authRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
