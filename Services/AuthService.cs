using BCrypt.Net;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null)
            {
                Console.WriteLine($"DEBUG: User '{loginDto.Username}' not found in database");
                return null;
            }

            Console.WriteLine($"DEBUG: Found user '{user.Username}', checking password...");
            Console.WriteLine($"DEBUG: Stored password hash: {user.Password}");
            Console.WriteLine($"DEBUG: Input password: {loginDto.Password}");

            // Verify password with enhanced error handling
            bool isPasswordValid;
            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
                Console.WriteLine($"DEBUG: BCrypt.Verify result: {isPasswordValid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: BCrypt.Verify failed with error: {ex.Message}");
                // If BCrypt fails, might be old hash format or corrupted data
                // For development, allow fallback to plain text comparison
                // In production, this should be removed
                if (user.Password == loginDto.Password)
                {
                    Console.WriteLine("DEBUG: Fallback to plain text comparison - MATCHED");
                    // Rehash the password with proper BCrypt
                    user.Password = BCrypt.Net.BCrypt.HashPassword(loginDto.Password, 12);
                    await _userRepository.UpdateAsync(user);
                    isPasswordValid = true;
                }
                else
                {
                    Console.WriteLine("DEBUG: Fallback to plain text comparison - NO MATCH");
                    throw new InvalidOperationException($"Password verification failed: {ex.Message}");
                }
            }

            if (!isPasswordValid)
            {
                Console.WriteLine("DEBUG: Password validation failed, returning null");
                return null;
            }

            Console.WriteLine("DEBUG: Password validation successful, generating token...");
            var token = _jwtService.GenerateToken(user);
            var expires = _jwtService.GetTokenExpiry();

            return new AuthResponseDto
            {
                Token = token,
                Expires = expires,
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    RoleName = user.RoleName,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: Login exception: {ex.Message}");
            throw new InvalidOperationException($"Login failed: {ex.Message}", ex);
        }
    }
}
