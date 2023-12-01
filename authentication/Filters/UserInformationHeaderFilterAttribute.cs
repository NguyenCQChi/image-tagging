using System.Net;
using authentication.Models;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class UserInformationHeaderFilterAttribute : ActionFilterAttribute
{
    private readonly ApiResponse _response;
    private readonly IConfiguration _configuration;
    
    public UserInformationHeaderFilterAttribute(IConfiguration configuration)
    {
        this._response = new ApiResponse();
        _configuration = configuration;
    }
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValue))
        {
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:MissingAuthHeader")!);
            context.Result = new ObjectResult(_response)
            {
                StatusCode = (int)HttpStatusCode.ExpectationFailed
            };
            return;
        }
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Refresh-Token", out var refreshHeaderValue))
        {
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:MissingRefreshHeader")!);
            context.Result = new ObjectResult(_response)
            {
                StatusCode = (int)HttpStatusCode.ExpectationFailed
            };
            return;
        }
        else
        {
            if (string.IsNullOrEmpty(refreshHeaderValue))
            {
                _response.StatusCode = HttpStatusCode.ExpectationFailed;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:NullRefreshHeader")!);
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.ExpectationFailed
                };
                return;
            }
        }
        
        base.OnActionExecuting(context);
    }
    
}