using System.Net;
using authentication.Data;
using authentication.Models;
using authentication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace authentication.Filters;

public class HeaderAccessTokenFilterAttribute : ActionFilterAttribute
{
    private readonly ApiResponse _response;
    private readonly ApplicationDbContext _db;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    
    public HeaderAccessTokenFilterAttribute(ApplicationDbContext db, IUserRepository userRepository, IConfiguration configuration)
    {
        this._response = new ApiResponse();
        _db = db;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues);
        context.HttpContext.Request.Headers.TryGetValue("X-Refresh-Token", out var refreshHeaderValues);
        var authorizationHeaderValue = authorizationHeaderValues.FirstOrDefault()?.Replace("Bearer", "").Trim();
        var refreshHeaderValue = refreshHeaderValues.FirstOrDefault()?.Trim();
        var existingRefreshToken = await _db.RefreshTokens!.FirstOrDefaultAsync(u => u.Refresh_Token == refreshHeaderValue);
        var isTokenValid = _userRepository.GetAccessTokenData(authorizationHeaderValue!, existingRefreshToken!.UserId, existingRefreshToken.JwtTokenId);
        if (!isTokenValid)
        {
            await _userRepository.MarkTokenAsInvalid(existingRefreshToken);
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add(_configuration.GetValue<string>("UserResponseStrings:UnableToVerifyIdentity")!);
            context.Result = new ObjectResult(_response)
            {
                StatusCode = (int)HttpStatusCode.ExpectationFailed
            };
            await base.OnActionExecutionAsync(context, next);
            return;
        }


        await base.OnActionExecutionAsync(context, next);
        return;
    }
    
}