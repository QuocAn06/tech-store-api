using Microsoft.AspNetCore.Mvc;
using TechStore.API.DTOs;
using TechStore.API.Services.Implementations;
using TechStore.API.Services.Interfaces;

namespace TechStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            var category = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryCreateDto dto)
        {
            var category = await _categoryService.UpdateAsync(id, dto);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _categoryService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }
            ;

            return NoContent();
        }
    }
}
