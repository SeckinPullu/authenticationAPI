using AuthenticationAPI.Models.Entites;
namespace AuthenticationAPI.Models
{
    public class AuthResponse
    {
        public AuthResponse(User request, string jwtToken , string refreshToken)
        {
            Id = request.Id;
            Name = request.Name;
            Surname = request.Surname;
            Username = request.Username;
            JwtToken = jwtToken;
            RefReshToken = refreshToken;
        }
        public int Id {get;set;}
        public string Name {get;set;}
        public string Surname {get;set;}
        public string Username {get;set;}
        public string JwtToken {get;set;}
        public string RefReshToken {get;set;}
    }
}