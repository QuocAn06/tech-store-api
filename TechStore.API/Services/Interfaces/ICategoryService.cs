using TechStore.API.DTOs;

namespace TechStore.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
        Task<CategoryDto?> UpdateAsync(int id, CategoryCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
