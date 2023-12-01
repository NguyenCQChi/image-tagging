using System.Net;
using authentication.Data;
using authentication.Models;
using authentication.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace authentication.Filters;

public class LoginUserValidationFilterAttribute : ActionFilterAttribute
{
    private readonly ApiResponse _response;
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    
    public LoginUserValidationFilterAttribute(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        this._response = new ApiResponse();
        _db = db;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("model", out var modelObject) && modelObject is LoginRequestDto model)
        {
            var user = await _db.ApplicationUsers!.FirstOrDefaultAsync(x => 
                string.Equals(x.UserName!, model.UserName!, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:IncorrectUserName")!);
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
                await base.OnActionExecutionAsync(context, next);
                return;
            }
            else
            {
                var isValid = await _userManager.CheckPasswordAsync(user, model.Password!);
                if (!isValid)
                {
                    _response.StatusCode = HttpStatusCode.Unauthorized;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:IncorrectPassword")!);
                    context.Result = new ObjectResult(_response)
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized
                    };
                    await base.OnActionExecutionAsync(context, next);
                    return;
                }
            }
        }
        await base.OnActionExecutionAsync(context, next);
        return;
    }
}