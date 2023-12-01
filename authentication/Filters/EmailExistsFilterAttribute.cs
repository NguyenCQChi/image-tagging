using System.Net;
using authentication.Models;
using authentication.Models.Dto;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class EmailExistsFilterAttribute : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository;
    private readonly ApiResponse _response;
    private readonly IConfiguration _configuration;
    
    public EmailExistsFilterAttribute(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        this._response = new ApiResponse();
        _configuration = configuration;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("email", out var emailObject) &&
            emailObject is string email)
        {
            var ifEmailUnique = _userRepository.IsUniqueEmail(email);
            if (ifEmailUnique)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:EmailNotFound")!);
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
        }
        base.OnActionExecuting(context);
    }
    
}