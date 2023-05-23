using Microsoft.EntityFrameworkCore;
using LOSAFAPI.Models;

namespace LOSAFAPI.Data
{
        public class ClaimItemDbContext : DbContext
        {
            public ClaimItemDbContext(DbContextOptions<ClaimItemDbContext> options) : base(options)
            {
            }

            public DbSet<ClaimItem> ClaimItem { get; set; }
        }
    
}
