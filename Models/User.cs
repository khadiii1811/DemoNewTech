using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = default!;
    
    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; } = default!;
    
    [Required]
    [StringLength(20)]
    public string RoleName { get; set; } = "customer"; // admin or customer
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}
