using Microsoft.EntityFrameworkCore;
using TechStore.API.Data;
using TechStore.API.Models;
using TechStore.API.Repositories.Interfaces;

namespace TechStore.API.Repositories.Implementations
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly TechStoreDbContext _context;
        public CategoryRepository(TechStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
