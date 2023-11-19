using System.ComponentModel.DataAnnotations;

namespace authentication.Models;

public class RefreshToken
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public required string UserId { get; set; }
    
    [Required]
    public required string JwtTokenId { get; set; }
    
    [Required]
    public required string Refresh_Token { get; set; }
    
    [Required]
    public bool IsValid { get; set; }
    public DateTime ExpiresAt { get; set; }
}