using System.Net;
using authentication.Models;
using authentication.Models.Dto;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class RegistrationEmailFilterAttribute : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository;
    private readonly ApiResponse _response;
    private readonly IConfiguration _configuration;
    
    public RegistrationEmailFilterAttribute(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        this._response = new ApiResponse();
        _configuration = configuration;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("model", out var modelObject) &&
            modelObject is RegistrationRequestDto model)
        {
            var ifEmailUnique = _userRepository.IsUniqueEmail(model.Email);
            if (!ifEmailUnique)
            {
                _response.StatusCode = HttpStatusCode.NotAcceptable;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:EmailInUse")!);
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable
                };
            }
        }
        base.OnActionExecuting(context);
    }
}