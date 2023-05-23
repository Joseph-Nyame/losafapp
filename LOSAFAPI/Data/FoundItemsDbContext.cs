using LOSAFAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LOSAFAPI.Data
{
    public class FoundItemsDbContext : DbContext
    {
        public FoundItemsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FoundItems>FoundItems{ get; set; }
    }
}
