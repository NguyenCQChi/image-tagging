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
    
    public EmailExistsFilterAttribute(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        this._response = new ApiResponse();
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
                _response.ErrorMessages.Add("Email not found.");
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
        }
        base.OnActionExecuting(context);
    }
    
}