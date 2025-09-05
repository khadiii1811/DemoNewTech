using CustomerManagement.Models;

namespace CustomerManagement.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    DateTime GetTokenExpiry();
}
