using English.Model.CERF;
using English.Model.FRE;
using Microsoft.EntityFrameworkCore;

namespace English.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
        public DbSet<FRE> FREs { get; set; }

    }
}
