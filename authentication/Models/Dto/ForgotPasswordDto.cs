using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace authentication.Models.Dto;

public class ForgotPasswordDto
{
    [Required]
    [PasswordPropertyText(true)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$", ErrorMessage = "Password Requirements: an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least six characters long.")]
    public required string Password { get; set; }
    
    [Required]
    [PasswordPropertyText(true)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$", ErrorMessage = "Password Requirements: an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least six characters long.")]
    public required string ConfirmPassword { get; set; }
    
    [Required]
    public required string Token { get; set; }
}