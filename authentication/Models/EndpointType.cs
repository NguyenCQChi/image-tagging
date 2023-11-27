using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication.Models;

public class EndpointType
{   
    [Key]
    public required string Id { get; set; }

    [Required]
    [MaxLength(1024)]
    public required string Name { get; set; }
    
    [Required]
    public required string RequestTypeId { get; set; }
    public RequestType RequestType { get; set; }
}