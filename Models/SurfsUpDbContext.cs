using Microsoft.EntityFrameworkCore;

namespace SurfsupEmil.Models
{
    public class SurfsUpDbContext : DbContext
    {
        public DbSet<Surfboard> Surfboards { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<User> Users { get; set; }

        public SurfsUpDbContext(DbContextOptions<SurfsUpDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Surfboards)
                .WithMany(s => s.Orders)
                .UsingEntity(j => j.ToTable("OrderSurfboard"));
        }
    }
}
