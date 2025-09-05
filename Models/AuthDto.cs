using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models;

public class LoginDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = default!;
    
    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; } = default!;
}

public class AuthResponseDto
{
    public string Token { get; set; } = default!;
    public DateTime Expires { get; set; }
    public UserResponseDto User { get; set; } = default!;
}
