using System.Net;
using authentication.Data;
using authentication.Models;
using authentication.Models.Dto;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace authentication.Filters;

public class ValidateRefreshTokenFilterAttribute : ActionFilterAttribute
{
    private readonly ApiResponse _response;
    private readonly ApplicationDbContext _db;
    private readonly IUserRepository _userRepository;
    
    public ValidateRefreshTokenFilterAttribute(ApplicationDbContext db, IUserRepository userRepository)
    {
        this._response = new ApiResponse();
        _db = db;
        _userRepository = userRepository;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("model", out var modelObject) && modelObject is TokenDto model)
        {
            // Find an existing refresh token
            var existingRefreshToken = await _db.RefreshTokens!.FirstOrDefaultAsync(u => u.Refresh_Token == model.RefreshToken);
            if (existingRefreshToken == null) {
                _response.StatusCode = HttpStatusCode.ExpectationFailed;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Unable to confirm your identity, please log in again.");
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.ExpectationFailed
                };
                await base.OnActionExecutionAsync(context, next);
                return;
            }
            // Is someone tried to use an expired refreshToken, log them out. (Possible Fraud)
            if (!existingRefreshToken.IsValid)
            {
                await _userRepository.MarkAllTokenInChainAsInvalid(existingRefreshToken.UserId,existingRefreshToken.JwtTokenId);
                _response.StatusCode = HttpStatusCode.ExpectationFailed;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Unable to verify your identity, please log in again.");
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.ExpectationFailed
                };
                await base.OnActionExecutionAsync(context, next);
                return;
            }
            // If the refresh token was valid but expired
            if (existingRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                await _userRepository.MarkTokenAsInvalid(existingRefreshToken);
                _response.StatusCode = HttpStatusCode.ExpectationFailed;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Session expired, please login again.");
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = (int)HttpStatusCode.ExpectationFailed
                };
                await base.OnActionExecutionAsync(context, next);
                return;
            }
        }
        await base.OnActionExecutionAsync(context, next);
        return;
    }
}