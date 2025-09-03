using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models;

public class Customer
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = default!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;
    
    [Phone]
    public string? Phone { get; set; }
    
    [StringLength(500)]
    public string? Address { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}
