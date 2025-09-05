using CustomerManagement.Models;

namespace CustomerManagement.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}
