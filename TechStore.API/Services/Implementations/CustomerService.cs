using Microsoft.AspNetCore.Http.HttpResults;
using TechStore.API.DTOs;
using TechStore.API.Models;
using TechStore.API.Repositories.Interfaces;
using TechStore.API.Services.Interfaces;

namespace TechStore.API.Services.Implementations
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<(IEnumerable<CustomerDto> Customers, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, string? search)
        {
            var customers = await _customerRepository.GetAllAsync(pageNumber, pageSize, search);
            var total = await _customerRepository.CountAsync(search);

            return (customers.Select(_customer => new CustomerDto
            {
                Id = _customer.Id,
                FullName = _customer.FullName,
                Email = _customer.Email,
                PhoneNumber = _customer.PhoneNumber,
                Address = _customer.Address
            }), total);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var query = await _customerRepository.GetByIdAsync(id);

            if (query == null)
            {
                return null;
            }

            return new CustomerDto
            {
                Id = query.Id,
                FullName = query.FullName,
                Email = query.Email,
                PhoneNumber = query.PhoneNumber,
                Address = query.Address
            };
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreateDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address
            };
            await _customerRepository.AddAsync(customer);

            var created = await _customerRepository.GetByIdAsync(customer.Id);

            return new CustomerDto
            {
                Id = created.Id,
                FullName = created.FullName,
                Email = created.Email,
                PhoneNumber = created.PhoneNumber,
                Address = created.Address
            };
        }

        public async Task<CustomerDto?> UpdateAsync(int id, CustomerCreateDto dto)
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.Address = dto.Address;

            await _customerRepository.UpdateAsync(existing);

            var updated = await _customerRepository.GetByIdAsync(existing.Id);

            return new CustomerDto
            {
                Id = updated.Id,
                FullName = updated.FullName,
                Email = updated.Email,
                PhoneNumber = updated.PhoneNumber,
                Address = updated.Address
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _customerRepository.DeleteAsync(existing);
            return true;
        }
    }
}
