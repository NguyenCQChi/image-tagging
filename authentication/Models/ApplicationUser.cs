using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace authentication.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MinLength(3)]
    [MaxLength(256)]
    public string? Name { get; set; }
}