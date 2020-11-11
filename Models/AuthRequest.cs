using System.ComponentModel.DataAnnotations;
namespace AuthenticationAPI.Models
{
    public class AuthRequest
    {
        [Required]
        public string Username {get;set;}
        
        [Required]
        public string Password {get;set;}
    }
}