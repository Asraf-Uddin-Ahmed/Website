using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Website.Identity.Aggregates;
using Website.Identity.Models;
namespace Website.Identity.Repositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<IdentityResult> CreateAsync(IdentityUser user);
        void Dispose();
        Task<IdentityUser> FindAsync(UserLoginInfo loginInfo);
        Client FindClient(string clientId);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        Task<IdentityUser> FindUser(string userName, string password);
        List<RefreshToken> GetAllRefreshTokens();
        Task<IdentityResult> RegisterUser(UserModel userModel);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
    }
}
