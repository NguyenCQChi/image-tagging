using System.Net;
using authentication.Models;
using authentication.Models.Dto;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class MatchPasswordsFilterAttribute : ActionFilterAttribute
{
    private readonly ApiResponse _response;
    private readonly IConfiguration _configuration;
    
    public MatchPasswordsFilterAttribute(IUserRepository userRepository,  IConfiguration configuration)
    {
        this._response = new ApiResponse();
        _configuration = configuration;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("model", out var modelObject) &&
            modelObject is ForgotPasswordDto model)
        {
            var areEqualCaseSensitive = string.Equals(model.Password, model.ConfirmPassword, StringComparison.Ordinal);
            if (!areEqualCaseSensitive)
            {
                _response.StatusCode = HttpStatusCode.NotAcceptable;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:PasswordsDontMatch")!);
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable
                };
            }
        }
        base.OnActionExecuting(context);
    }
}