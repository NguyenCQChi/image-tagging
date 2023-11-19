using Microsoft.Build.Framework;

namespace authentication.Models;

public class ForgotPasswordEmail
{
    [Required]
    public required string ResetLink { get; set; }
    
    [Required]
    public required string ResetToken { get; set; }
    
    [Required]
    public required string Title { get; set; }
}