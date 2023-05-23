using LOSAFAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LOSAFAPI.Data
{
    public class LogItemDbContext :DbContext
    {
        public LogItemDbContext(DbContextOptions<LogItemDbContext> options) : base(options)
        {
        }

        public DbSet<LogItem> LogItem { get; set; }
    }
}




