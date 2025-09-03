using Microsoft.AspNetCore.Mvc;
using CustomerManagement.Models;
using CustomerManagement.Services;

namespace CustomerManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: api/customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetCustomers()
    {
        try
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving customers.", error = ex.Message });
        }
    }

    // GET: api/customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> GetCustomer(int id)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = $"Customer with ID {id} not found." });
            }

            return Ok(customer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the customer.", error = ex.Message });
        }
    }

    // POST: api/customers
    [HttpPost]
    public async Task<ActionResult<CustomerResponseDto>> CreateCustomer(CreateCustomerDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.CreateCustomerAsync(createDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the customer.", error = ex.Message });
        }
    }

    // PUT: api/customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto updateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.UpdateCustomerAsync(id, updateDto);
            if (customer == null)
            {
                return NotFound(new { message = $"Customer with ID {id} not found." });
            }

            return Ok(customer);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the customer.", error = ex.Message });
        }
    }

    // DELETE: api/customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        try
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Customer with ID {id} not found." });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the customer.", error = ex.Message });
        }
    }

    // PATCH: api/customers/5/deactivate
    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> DeactivateCustomer(int id)
    {
        try
        {
            var result = await _customerService.DeactivateCustomerAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Customer with ID {id} not found." });
            }

            return Ok(new { message = "Customer deactivated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deactivating the customer.", error = ex.Message });
        }
    }

    // PATCH: api/customers/5/activate
    [HttpPatch("{id}/activate")]
    public async Task<IActionResult> ActivateCustomer(int id)
    {
        try
        {
            var result = await _customerService.ActivateCustomerAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Customer with ID {id} not found." });
            }

            return Ok(new { message = "Customer activated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while activating the customer.", error = ex.Message });
        }
    }
}
