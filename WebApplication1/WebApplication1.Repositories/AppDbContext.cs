using Microsoft.EntityFrameworkCore;

using WebApplication1.Domain;

namespace WebApplication1.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                    .HasMany(o => o.Orders)
                    .WithMany(p => p.Products)
                    .UsingEntity<OrderProduct>(
                        x => x.HasOne<Order>().WithMany().HasForeignKey(x => x.OrderId),
                        x => x.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId)
                    );
        }
    }
}
