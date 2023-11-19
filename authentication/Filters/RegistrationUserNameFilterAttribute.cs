using System.Net;
using authentication.Models;
using authentication.Models.Dto;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class RegistrationUserNameFilterAttribute : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository;
    private readonly ApiResponse _response;
    
    public RegistrationUserNameFilterAttribute(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        this._response = new ApiResponse();
    }

    public override async Task<Task> OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("model", out var modelObject) && modelObject is RegistrationRequestDto model)
        {
            var ifUserNameUnique = _userRepository.IsUniqueUser(model.UserName!);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.NotAcceptable;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists.");
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable
                };
                return base.OnActionExecutionAsync(context, null!);
            }
            var resultContext = await next();
            if (resultContext.Exception == null)
            {
                if (_userRepository.IsUniqueUser(model.UserName!))
                {
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Error while registering.");
                    resultContext.Result = new ObjectResult(_response)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }
            }
        }
        return base.OnActionExecutionAsync(context, next);
    }
}