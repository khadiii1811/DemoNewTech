using Microsoft.AspNetCore.Mvc;
using CustomerManagement.Models;
using CustomerManagement.Services;

namespace CustomerManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly IUserService _userService;

    public SeedController(IUserService userService)
    {
        _userService = userService;
    }

    // POST: api/seed/admin
    [HttpPost("admin")]
    public async Task<IActionResult> CreateAdminUser()
    {
        try
        {
            Console.WriteLine("DEBUG: SeedController - CreateAdminUser called");
            
            var adminDto = new CreateUserDto
            {
                Username = "admin",
                Password = "adminpass123",
                RoleName = "admin"
            };

            Console.WriteLine($"DEBUG: SeedController - Creating admin user with username: {adminDto.Username}");
            
            var admin = await _userService.CreateUserAsync(adminDto);
            
            Console.WriteLine($"DEBUG: SeedController - Admin user created successfully with ID: {admin.Id}");
            
            return Ok(new { message = "Admin user created successfully.", user = admin });
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"DEBUG: SeedController - InvalidOperationException: {ex.Message}");
            // This will catch the "username already exists" error
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: SeedController - Exception: {ex.Message}");
            Console.WriteLine($"DEBUG: SeedController - Stack trace: {ex.StackTrace}");
            return StatusCode(500, new { message = "An error occurred while creating admin user.", error = ex.Message });
        }
    }
}
