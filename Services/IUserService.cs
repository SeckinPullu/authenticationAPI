using AuthenticationAPI.Models;
using AuthenticationAPI.Models.Entites;
namespace AuthenticationAPI.Services
{
    public interface IUserService
    {
        AuthResponse Authenticate(AuthRequest req , string ipAddress);
        AuthResponse RefreshToken(RefreshTokenRequest req, string ipAddress);
        bool RevokeRefreshToken(RefreshTokenRequest request, string ipAddress);
        
    }
}