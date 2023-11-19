using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace authentication.Models.Dto;

public class RegistrationRequestDto
{
    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Usernames can only have (A-Z), (a-z), (underscore), (hyphen).")]
    public required string UserName { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(256)]
    [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only (A-Z), (a-z) and spaces are allowed for name.")]
    public required string Name { get; set; }
    
    [Required]
    [PasswordPropertyText(true)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$", ErrorMessage = "Password Requirements: an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least six characters long.")]
    
    public required string Password { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(256)]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only (A-Z), (a-z) allowed for Role.")]
    public required string Role { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}