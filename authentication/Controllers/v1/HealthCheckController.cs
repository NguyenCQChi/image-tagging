using System.Net;
using Asp.Versioning;
using authentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace authentication.Controllers.v1;


[Route("/")]
[ApiController]
[ApiVersion("1.0")]
public class HealthCheckController : ControllerBase
{
    private readonly ApiResponse _response;

    public HealthCheckController()
    {
        this._response = new ApiResponse();
    }

    [HttpGet("health")]
    [HttpGet("api/v{version:ApiVersion}/health")]
    [Produces("application/json")]
    public async Task<IActionResult> HealthCheck()
    {
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = "Healthy and Reachable";
        return Ok(_response);
    }
}