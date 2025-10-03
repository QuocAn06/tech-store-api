using Microsoft.EntityFrameworkCore;
using TechStore.API.Models;

namespace TechStore.API.Data
{
    public class TechStoreDbContext: DbContext
    {
        public TechStoreDbContext(DbContextOptions<TechStoreDbContext> options): base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Cấu hình quan hệ 1-nhiều
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
