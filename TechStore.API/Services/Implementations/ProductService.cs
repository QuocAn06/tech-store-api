using TechStore.API.DTOs;
using TechStore.API.Models;
using TechStore.API.Repositories.Interfaces;
using TechStore.API.Services.Interfaces;

namespace TechStore.API.Services.Implementations
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, string? search)
        {
            var products = await _productRepository.GetAllAsync(pageNumber, pageSize, search);
            var total = await _productRepository.CountAsync(search);

            return (products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name
            }), total);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _productRepository.GetByIdAsync(id);

            if (p == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name
            };
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
            await _productRepository.AddAsync(product);

            var created = await _productRepository.GetByIdAsync(product.Id);

            return new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Price = created.Price,
                CategoryName = created.Category.Name
            };
        }

        public async Task<ProductDto?> UpdateAsync(int id, ProductCreateDto dto)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.CategoryId = dto.CategoryId;

            await _productRepository.UpdateAsync(existing);

            var updated = await _productRepository.GetByIdAsync(existing.Id);

            return new ProductDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Price = updated.Price,
                CategoryName = updated.Category.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _productRepository.DeleteAsync(existing);
            return true;
        }
    }
}
