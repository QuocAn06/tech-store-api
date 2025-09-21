using Microsoft.AspNetCore.Mvc;
using TechStore.API.DTOs;
using TechStore.API.Services.Interfaces;

namespace TechStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber =1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var (products, total) = await _productService.GetAllAsync(pageNumber, pageSize, search);
            return Ok(new
            {
                TotalCount = total,
                Data = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            var product = await _productService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCreateDto dto)
        {
            var product = await _productService.UpdateAsync(id, dto);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteAsync(id);

            if (!success) 
            { 
                return NotFound(); 
            }; 
            
            return NoContent();
        }
    }
}
