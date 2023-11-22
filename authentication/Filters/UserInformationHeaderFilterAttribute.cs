using System.Net;
using authentication.Models;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class UserInformationHeaderFilterAttribute : ActionFilterAttribute
{
    private readonly ApiResponse _response;
    
    public UserInformationHeaderFilterAttribute()
    {
        this._response = new ApiResponse();
    }
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValue))
        {
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Missing Header: Authorization");
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
            _response.ErrorMessages.Add("Missing Header: X-Refresh-Token");
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
                _response.ErrorMessages.Add("Header: X-Refresh-Token is null or empty");
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