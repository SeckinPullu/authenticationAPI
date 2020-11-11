using System.ComponentModel.DataAnnotations;
namespace AuthenticationAPI.Models
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken {get;set;}
    }
}