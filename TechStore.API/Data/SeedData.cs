using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechStore.API.Models;
using System;
using System.Linq;

namespace TechStore.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TechStoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TechStoreDbContext>>()))
            {
                // Nếu đã có dữ liệu thì bỏ qua
                if (context.Categories.Any() || context.Products.Any())
                {
                    return;
                }

                // Seed Categories
                var categories = new[]
                {
                    new Category { Name = "Laptop" },
                    new Category { Name = "Smartphone" },
                    new Category { Name = "Tablet" },
                    new Category { Name = "Accessory" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();

                // Seed Products
                var products = new[]
                {
                    new Product { Name = "MacBook Pro 14", Price = 2500m, CategoryId = categories[0].Id },
                    new Product { Name = "Dell XPS 13", Price = 1800m, CategoryId = categories[0].Id },
                    new Product { Name = "iPhone 15 Pro", Price = 1200m, CategoryId = categories[1].Id },
                    new Product { Name = "Samsung Galaxy S24", Price = 1100m, CategoryId = categories[1].Id },
                    new Product { Name = "iPad Air", Price = 800m, CategoryId = categories[2].Id },
                    new Product { Name = "Logitech MX Master 3S", Price = 120m, CategoryId = categories[3].Id }
                };

                context.Products.AddRange(products);
                context.SaveChanges();

                // Seed Customers
                var customers = new[]
                {
                    new Customer { FullName = "Nguyễn Văn A", Email = "nguyenvana@email.com", PhoneNumber = "0912345678", Address = "123 Đường Lê Lợi, Quận 1, TP.HCM", CreatedAt = DateTime.UtcNow },
                    new Customer { FullName = "Trần Thị B", Email = "tranthib@email.com", PhoneNumber = "0923456789", Address = "456 Đường Nguyễn Huệ, Quận 1, TP.HCM", CreatedAt = DateTime.UtcNow },
                    new Customer { FullName = "Lê Văn C", Email = "levanc@email.com",PhoneNumber = "0934567890", Address = "789 Đường Pasteur, Quận 3, TP.HCM", CreatedAt = DateTime.UtcNow },
                    new Customer { FullName = "Phạm Thị D", Email = "phamthid@email.com", PhoneNumber = "0945678901", Address = "321 Đường Cách Mạng Tháng 8, Quận 10, TP.HCM", CreatedAt = DateTime.UtcNow },
                    new Customer { FullName = "Hoàng Văn E", Email = "hoangvane@email.com", PhoneNumber = "0956789012", Address = "654 Đường 3/2, Quận 10, TP.HCM", CreatedAt = DateTime.UtcNow }
                 };

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }
    }
}
