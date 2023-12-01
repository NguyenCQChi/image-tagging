using System.Net;
using authentication.Models;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class UserNameExistsFilterAttribute : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository;
    private readonly ApiResponse _response;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    
    public UserNameExistsFilterAttribute(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userRepository = userRepository;
        this._response = new ApiResponse();
        _userManager = userManager;
        _configuration = configuration;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("userName", out var userNameObject) &&
            userNameObject is string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:NoUserForGivenUserName")!);
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