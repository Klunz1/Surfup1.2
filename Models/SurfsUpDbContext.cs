using Microsoft.EntityFrameworkCore;

namespace SurfsupEmil.Models
{
    public class SurfsUpDbContext : DbContext
    {
        public DbSet<Surfboard> Surfboards { get; set; }
        public DbSet<Order> Orders { get; set; }

        public SurfsUpDbContext(DbContextOptions<SurfsUpDbContext> options) : base(options)
        {

        }
    }
}
