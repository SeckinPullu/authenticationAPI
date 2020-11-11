using System;
using AuthenticationAPI.Models.Entites;
using System.Linq;
using AuthenticationAPI.Models;
using AuthenticationAPI.Helpers;

namespace AuthenticationAPI.Services
{
    public class UserService: IUserService
    {
        private readonly UserContext _context;
        private readonly IHelpers _helpers;
        public UserService(UserContext context, IHelpers helpers)
        {
            _context = context;
            _helpers = helpers;
        }
        public AuthResponse Authenticate(AuthRequest request, string ipAddress)
        {
            var user = _context.Users.FirstOrDefault(d => d.Username == request.Username && 
                d.Password == request.Password.EncryptString());
            if(user != null)
            {
                var jwt = _helpers.GenerateJSONWebToken(user);
                var refresh = _helpers.GenerateRefreshToken(ipAddress);
                UpdateUserRefreshToken(user, refresh);
                return new AuthResponse(
                    user,
                    jwt,
                    refresh.Token
                );
            }
            else
                return null;
        }
        public AuthResponse RefreshToken(RefreshTokenRequest req , string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(d => d.RefreshTokens.Any(t => t.Token == req.RefreshToken));
            if(user != null)
            {
                var refresh = user.RefreshTokens.Single(t => t.Token == req.RefreshToken);
                if(!refresh.IsActive)
                    return null;
                var newRefresh = _helpers.GenerateRefreshToken(ipAddress);
                refresh.Revoked = DateTime.UtcNow;
                refresh.RevokedByIp = ipAddress;
                refresh.ReplacedByToken = newRefresh.Token;
                UpdateUserRefreshToken(user, newRefresh);
                var jwt = _helpers.GenerateJSONWebToken(user);
                return new AuthResponse(
                    user,
                    jwt,
                    newRefresh.Token
                );
            }
            else
                return null;
        }
        public bool RevokeRefreshToken(RefreshTokenRequest req, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(d => 
                d.RefreshTokens.Any(t => t.Token == req.RefreshToken)
            );
            if(user == null)
                return false;
            var refresh = user.RefreshTokens.Single(t => t.Token == req.RefreshToken);
            if(!refresh.IsActive)
                return false;
            refresh.Revoked = DateTime.UtcNow;
            refresh.RevokedByIp = ipAddress;
            UpdateUserRefreshToken(user);
            return true;

        }

        private void UpdateUserRefreshToken(User user, RefreshToken newRefreshToken = null)
        {
            try
            {
                if(newRefreshToken != null)
                    user.RefreshTokens.Add(newRefreshToken);
                _context.Update(user);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}