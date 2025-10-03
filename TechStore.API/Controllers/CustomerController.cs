using Microsoft.AspNetCore.Mvc;
using TechStore.API.DTOs;
using TechStore.API.Services.Interfaces;

namespace TechStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber =1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var (customers, total) = await _customerService.GetAllAsync(pageNumber, pageSize, search);
            return Ok(new
            {
                TotalCount = total,
                Data = customers
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateDto dto)
        {
            var product = await _customerService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerCreateDto dto)
        {
            var customer = await _customerService.UpdateAsync(id, dto);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _customerService.DeleteAsync(id);

            if (!success) 
            { 
                return NotFound(); 
            }; 
            
            return NoContent();
        }
    }
}
