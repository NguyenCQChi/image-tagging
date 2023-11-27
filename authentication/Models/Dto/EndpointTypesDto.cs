using System.ComponentModel.DataAnnotations;

namespace authentication.Models.Dto;

public class EndpointTypesDto
{
    [Required]
    [MaxLength(1024)]
    public required string Name { get; set; }
    
    [Required]
    public required string RequestTypeName { get; set; }
}