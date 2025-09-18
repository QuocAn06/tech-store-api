using Microsoft.EntityFrameworkCore;
using TechStore.API.Models;

namespace TechStore.API.Data
{
    public class TechStoreDbContext: DbContext
    {
        public TechStoreDbContext(DbContextOptions<TechStoreDbContext> options): base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
