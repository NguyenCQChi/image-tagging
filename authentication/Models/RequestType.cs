using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication.Models;

public class RequestType
{
    [Key]
    public required string Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string TypeName { get; set; }
}