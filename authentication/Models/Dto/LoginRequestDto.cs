using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace authentication.Models.Dto;

public class LoginRequestDto
{
    [Required]
    public required string UserName { get; set; }
    
    [Required]
    [PasswordPropertyText(true)]
    public required string Password { get; set; }
}