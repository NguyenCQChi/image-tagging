using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace authentication.Models;

[PrimaryKey(nameof(UserId), nameof(EndpointTypeId))]
public class UserEndpointRequests
{
    [Key]
    [Column(Order = 0)]
    public required string UserId { get; set; }

    [Key]
    [Column(Order = 1)]
    public required string EndpointTypeId { get; set; }

    public ApplicationUser User { get; set; }
    public EndpointType EndpointType { get; set; }

    public int NumRequests { get; set; } = 0;
}