using LOSAFAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LOSAFAPI.Data
{
    public class AddRegisterItemsDbContext : DbContext
    {
        public AddRegisterItemsDbContext(DbContextOptions<AddRegisterItemsDbContext> options) : base(options)
        {
        }

        public DbSet<AddItemsRegisteration> ItemsRegisteration { get; set; }
    }
}
