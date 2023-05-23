using LOSAFAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LOSAFAPI.Data
{
    public class UserApiDbContext : DbContext
    {
        public UserApiDbContext(DbContextOptions<UserApiDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
