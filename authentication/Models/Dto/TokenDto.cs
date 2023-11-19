using System.ComponentModel.DataAnnotations;

namespace authentication.Models.Dto;

public class TokenDto
{
    [Required]
    [MinLength(10)]
    public required string RefreshToken { get; set; }
    
    [Required]
    [MinLength(10)]
    public string? AccessToken { get; set; }
}