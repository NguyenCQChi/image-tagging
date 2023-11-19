using System.Net;
using Microsoft.Build.Framework;

namespace authentication.Models;

public class ApiResponse
{
    public ApiResponse()
    {
        ErrorMessages = new List<string>();
    }
    
    [Required]
    public HttpStatusCode StatusCode { get; set; }
    
    [Required]
    public bool IsSuccess { get; set; } = true;
    
    public List<string> ErrorMessages { get; set; }
    
    public object? Result { get; set; }
}