using CustomerManagement.Models;

namespace CustomerManagement.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync();
    Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);
    Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto createDto);
    Task<CustomerResponseDto?> UpdateCustomerAsync(int id, UpdateCustomerDto updateDto);
    Task<bool> DeleteCustomerAsync(int id);
    Task<bool> DeactivateCustomerAsync(int id);
    Task<bool> ActivateCustomerAsync(int id);
}
