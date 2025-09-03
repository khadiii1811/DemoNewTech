using CustomerManagement.Models;
using CustomerManagement.Repositories;

namespace CustomerManagement.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select(MapToResponseDto);
    }

    public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer != null ? MapToResponseDto(customer) : null;
    }

    public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto createDto)
    {
        // Check if email already exists
        var existingCustomer = await _customerRepository.GetByEmailAsync(createDto.Email);
        if (existingCustomer != null)
        {
            throw new InvalidOperationException($"Customer with email '{createDto.Email}' already exists.");
        }

        var customer = new Customer
        {
            Name = createDto.Name,
            Email = createDto.Email,
            Phone = createDto.Phone,
            Address = createDto.Address,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var createdCustomer = await _customerRepository.CreateAsync(customer);
        return MapToResponseDto(createdCustomer);
    }

    public async Task<CustomerResponseDto?> UpdateCustomerAsync(int id, UpdateCustomerDto updateDto)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            return null;

        // Check if email is being changed and if it already exists
        if (!string.IsNullOrEmpty(updateDto.Email) && updateDto.Email != customer.Email)
        {
            var existingCustomer = await _customerRepository.GetByEmailAsync(updateDto.Email);
            if (existingCustomer != null)
            {
                throw new InvalidOperationException($"Customer with email '{updateDto.Email}' already exists.");
            }
        }

        // Update only provided fields
        if (!string.IsNullOrEmpty(updateDto.Name))
            customer.Name = updateDto.Name;
        
        if (!string.IsNullOrEmpty(updateDto.Email))
            customer.Email = updateDto.Email;
        
        if (updateDto.Phone != null)
            customer.Phone = updateDto.Phone;
        
        if (updateDto.Address != null)
            customer.Address = updateDto.Address;
        
        if (updateDto.IsActive.HasValue)
            customer.IsActive = updateDto.IsActive.Value;

        var updatedCustomer = await _customerRepository.UpdateAsync(customer);
        return MapToResponseDto(updatedCustomer);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        return await _customerRepository.DeleteAsync(id);
    }

    public async Task<bool> DeactivateCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            return false;

        customer.IsActive = false;
        await _customerRepository.UpdateAsync(customer);
        return true;
    }

    public async Task<bool> ActivateCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            return false;

        customer.IsActive = true;
        await _customerRepository.UpdateAsync(customer);
        return true;
    }

    private static CustomerResponseDto MapToResponseDto(Customer customer)
    {
        return new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address,
            IsActive = customer.IsActive,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt
        };
    }
}
