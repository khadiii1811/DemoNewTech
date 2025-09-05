using UserManagement.Models;

namespace UserManagement.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}
