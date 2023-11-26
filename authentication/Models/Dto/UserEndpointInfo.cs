using Microsoft.Build.Framework;

namespace authentication.Models.Dto;

public class UserEndpointInfo
{
    [Required]
    public string? Id { get; set; }
    
    [Required]
    public string? UserName { get; set; }
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Email { get; set; }
    
    public Dictionary<string, int> EndpointInfo { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime? ExpiresAt { get; set; }
}