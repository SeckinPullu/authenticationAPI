using Microsoft.EntityFrameworkCore;
using AuthenticationAPI.Models.Entites;
namespace AuthenticationAPI.Models
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options): base(options){}
        public DbSet<User> Users {get;set;}
    }
}