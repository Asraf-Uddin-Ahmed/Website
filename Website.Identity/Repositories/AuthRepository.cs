using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Website.Identity;
using Website.Identity.Aggregates;
using Website.Identity.Models;

namespace Website.Identity.Repositories
{

    public class AuthRepository : IDisposable, IAuthRepository
    {
        private AuthDbContext _authDbContext;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _authDbContext = new AuthDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_authDbContext));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _authDbContext.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

           var existingToken = _authDbContext.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

           if (existingToken != null)
           {
             var result = await RemoveRefreshToken(existingToken);
           }
          
            _authDbContext.RefreshTokens.Add(token);

            return await _authDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
           var refreshToken = await _authDbContext.RefreshTokens.FindAsync(refreshTokenId);

           if (refreshToken != null) {
               _authDbContext.RefreshTokens.Remove(refreshToken);
               return await _authDbContext.SaveChangesAsync() > 0;
           }

           return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _authDbContext.RefreshTokens.Remove(refreshToken);
             return await _authDbContext.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _authDbContext.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
             return  _authDbContext.RefreshTokens.ToList();
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _authDbContext.Dispose();
            _userManager.Dispose();

        }
    }
}