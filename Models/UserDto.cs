using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models;

public class CreateUserDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = default!;
    
    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; } = default!;
    
    [Required]
    [RegularExpression("^(admin|customer)$", ErrorMessage = "RoleName must be either 'admin' or 'customer'")]
    public string RoleName { get; set; } = "customer";
}

public class UpdateUserDto
{
    [StringLength(50, MinimumLength = 3)]
    public string? Username { get; set; }
    
    [StringLength(255, MinimumLength = 6)]
    public string? Password { get; set; }
    
    [RegularExpression("^(admin|customer)$", ErrorMessage = "RoleName must be either 'admin' or 'customer'")]
    public string? RoleName { get; set; }
}

public class UserResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string RoleName { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
