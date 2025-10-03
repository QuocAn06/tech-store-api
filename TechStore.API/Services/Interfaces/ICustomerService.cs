using TechStore.API.DTOs;

namespace TechStore.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<(IEnumerable<CustomerDto> Customers, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, string? search);
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CustomerCreateDto dto);
        Task<CustomerDto?> UpdateAsync(int id, CustomerCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
