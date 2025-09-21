using TechStore.API.DTOs;

namespace TechStore.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, string? search);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateDto dto);
        Task<ProductDto?> UpdateAsync(int id, ProductCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
