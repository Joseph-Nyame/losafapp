using LOSAFAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LOSAFAPI.Data
{
    public class ReportItemsDbContext : DbContext
    {
        public ReportItemsDbContext(DbContextOptions<ReportItemsDbContext> options) : base(options)
        {
        }

        public DbSet<ReportItems> ReportItems { get; set; }
    }
}
