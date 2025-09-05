using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserManagement.Models;
using UserManagement.Services;
using System.Security.Claims;

namespace UserManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authResponse = await _authService.LoginAsync(loginDto);
            if (authResponse == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            return Ok(authResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
        }
    }

    // GET: api/auth/me
    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(usernameClaim) || string.IsNullOrEmpty(roleClaim))
            {
                return Unauthorized(new { message = "Invalid token." });
            }

            return Ok(new
            {
                id = userIdClaim,
                username = usernameClaim,
                role = roleClaim
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving user information.", error = ex.Message });
        }
    }
}
