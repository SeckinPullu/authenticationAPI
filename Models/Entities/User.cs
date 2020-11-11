using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Models.Entites
{
    public class User
    {
        [Key]
        public int Id {get;set;}
        public string Username {get;set;}
        public string Password {get;set;}
        public string Name {get;set;}
        public string Surname {get;set;}
        public List<RefreshToken> RefreshTokens {get;set;}
    }
}