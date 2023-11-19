using System.Net;
using authentication.Models;
using authentication.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication.Filters;

public class RegistrationRoleFilterAttribute : ActionFilterAttribute
{
    private readonly List<string> _roles;
    private readonly ApiResponse _response;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegistrationRoleFilterAttribute(IConfiguration configuration, RoleManager<IdentityRole> roleManager)
    {
        _roles = configuration.GetSection("ApiSettings:Roles").Get<List<string>>()!;
        this._response = new ApiResponse();
        _roleManager = roleManager;
    }
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("model", out var modelObject) &&
            modelObject is RegistrationRequestDto model)
        {
            if (!(_roleManager.RoleExistsAsync(model.Role!).GetAwaiter().GetResult()))
            {
                _response.StatusCode = HttpStatusCode.Forbidden;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("( \u0361\u00b0 \u035cʖ \u0361\u00b0) Allowed Roles: " + string.Join(", ", _roles));
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.Forbidden
                };
            }
            if (model.Role! == "admin")
            {
                _response.StatusCode = HttpStatusCode.Forbidden;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("﴾\u0361๏\u032f\u0361๏﴿ O'RLY? You're not allowed to set your role as Admin. We're stupid but not that stupid.");
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.Forbidden
                };
            }
        }

        base.OnActionExecuting(context);
    }
}