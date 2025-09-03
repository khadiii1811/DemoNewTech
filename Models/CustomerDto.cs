using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models;

public class CreateCustomerDto
{
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
}

public class UpdateCustomerDto
{
    [StringLength(100, MinimumLength = 2)]
    public string? Name { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Phone]
    public string? Phone { get; set; }
    
    [StringLength(500)]
    public string? Address { get; set; }
    
    public bool? IsActive { get; set; }
}

public class CustomerResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
