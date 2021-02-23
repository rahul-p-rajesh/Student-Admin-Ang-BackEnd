using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> opt) : base(opt)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
    }
}
