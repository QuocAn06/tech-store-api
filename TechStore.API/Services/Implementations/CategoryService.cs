using TechStore.API.DTOs;
using TechStore.API.Models;
using TechStore.API.Repositories.Interfaces;
using TechStore.API.Services.Interfaces;

namespace TechStore.API.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var c = await _categoryRepository.GetByIdAsync(id);

            if (c == null)
            {
                return null;
            }

            return new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            };
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            await _categoryRepository.AddAsync(category);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto?> UpdateAsync(int id, CategoryCreateDto dto)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = dto.Name;

            await _categoryRepository.UpdateAsync(existing);

            return new CategoryDto
            {
                Id = existing.Id,
                Name = existing.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _categoryRepository.DeleteAsync(existing);
            return true;
        }
    }
}
