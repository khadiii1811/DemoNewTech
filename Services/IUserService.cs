using UserManagement.Models;

namespace UserManagement.Services;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto?> GetUserByIdAsync(int id);
    Task<UserResponseDto> CreateUserAsync(CreateUserDto createDto);
    Task<UserResponseDto?> UpdateUserAsync(int id, UpdateUserDto updateDto);
    Task<bool> DeleteUserAsync(int id);
}
