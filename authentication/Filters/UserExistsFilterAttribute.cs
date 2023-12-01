using System.Net;
using authentication.Models;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class UserExistsFilterAttribute : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository;
    private readonly ApiResponse _response;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    
    public UserExistsFilterAttribute(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userRepository = userRepository;
        this._response = new ApiResponse();
        _userManager = userManager;
        _configuration = configuration;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("email", out var emailObject) &&
            emailObject is string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:NoUserForGivenEmail")!);
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
                await base.OnActionExecutionAsync(context, next);
                return;
            }
        }


        await base.OnActionExecutionAsync(context, next);
        return;
    }
}