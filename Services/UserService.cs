using UserManagement.Models;
using UserManagement.Repositories;
using BCrypt.Net;

namespace UserManagement.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(MapToResponseDto);
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user != null ? MapToResponseDto(user) : null;
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto createDto)
    {
        // Check if username already exists
        var existingUser = await _userRepository.GetByUsernameAsync(createDto.Username);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"User with username '{createDto.Username}' already exists.");
        }

        var user = new User
        {
            Username = createDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(createDto.Password, 12),
            RoleName = createDto.RoleName,
            CreatedAt = DateTime.UtcNow
        };

        var createdUser = await _userRepository.CreateAsync(user);
        return MapToResponseDto(createdUser);
    }

    public async Task<UserResponseDto?> UpdateUserAsync(int id, UpdateUserDto updateDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;

        // Check if username is being changed and if it already exists
        if (!string.IsNullOrEmpty(updateDto.Username) && updateDto.Username != user.Username)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(updateDto.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"User with username '{updateDto.Username}' already exists.");
            }
        }

        // Update only provided fields
        if (!string.IsNullOrEmpty(updateDto.Username))
            user.Username = updateDto.Username;
        
        if (!string.IsNullOrEmpty(updateDto.Password))
            user.Password = BCrypt.Net.BCrypt.HashPassword(updateDto.Password, 12);
        
        if (!string.IsNullOrEmpty(updateDto.RoleName))
            user.RoleName = updateDto.RoleName;

        var updatedUser = await _userRepository.UpdateAsync(user);
        return MapToResponseDto(updatedUser);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    private static UserResponseDto MapToResponseDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            RoleName = user.RoleName,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}
