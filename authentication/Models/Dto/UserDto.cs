using System.ComponentModel.DataAnnotations;

namespace authentication.Models.Dto;

public class UserDto
{
    [Required]
    public string? Id { get; set; }
    
    [Required]
    public string? UserName { get; set; }
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Email { get; set; }
}