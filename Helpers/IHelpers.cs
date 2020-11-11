using System;
using AuthenticationAPI.Models.Entites;
using AuthenticationAPI.Models;
namespace AuthenticationAPI.Helpers
{
    public interface IHelpers
    {
        string GenerateJSONWebToken(User userinfo);
        RefreshToken GenerateRefreshToken(string ip);

    }
}