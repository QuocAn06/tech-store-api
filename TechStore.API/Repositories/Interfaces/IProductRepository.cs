using TechStore.API.Models;

namespace TechStore.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize, string? search);
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<int> CountAsync(string? search);
    }
}
