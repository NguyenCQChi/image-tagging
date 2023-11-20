using System.Net;
using Asp.Versioning;
using authentication.Filters;
using authentication.Models;
using authentication.Models.Dto;
using authentication.Repository.IRepository;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.SendGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Swashbuckle.AspNetCore.Annotations;

namespace authentication.Controllers.v1;

[Route("api/v{version:ApiVersion}/auth")]
[ApiController]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ApiResponse _response;
    private readonly string _emailSender;
    private readonly string _forgotPasswordSubject;
    private readonly bool _secureCookies;
    private readonly IWebHostEnvironment _env;

    public UsersController(IUserRepository userRepository, IConfiguration configuration, IWebHostEnvironment env)
    {
        _userRepository = userRepository;
        this._response = new ApiResponse();
        _emailSender = configuration.GetValue<string>("EmailSettings:ForgotPasswordSender")!;
        _forgotPasswordSubject = configuration.GetValue<string>("EmailSettings:ForgotPasswordSubject")!;
        _secureCookies = configuration.GetValue<bool>("ApiSettings:SecureCookies")!;
        _env = env;
    }
    
    [HttpPost("register")]
    [ServiceFilter(typeof(RegistrationRoleFilterAttribute))]
    [ServiceFilter(typeof(RegistrationEmailFilterAttribute))]
    [ServiceFilter(typeof(RegistrationUserNameFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {
        await _userRepository.Register(model);
        _response.StatusCode = HttpStatusCode.Created;
        _response.IsSuccess = true;
        return StatusCode((int)HttpStatusCode.Created, _response);
    }
    
    [HttpPost("login")]
    [ServiceFilter(typeof(LoginUserValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var tokenDto = await _userRepository.Login(model);
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = tokenDto;
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = tokenDto;
        Response.Cookies.Append("refreshToken", tokenDto!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });

        Response.Cookies.Append("accessToken", tokenDto.AccessToken!, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });
        
        return Ok(_response);
    }
    
    [SwaggerOperation(Description = "Using nodejs fetch, make a post request with header 'Authorization': " +
                                    "`Bearer \n${jwtToken}`")]
    [HttpPost("validate")]
    [Authorize]
    [ServiceFilter(typeof(ValidateRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(AccessTokenValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public Task<IActionResult> ValidateTokens([FromBody] TokenDto model)
    {
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = "Authentication Successful!";
        return Task.FromResult<IActionResult>(Ok(_response));
    }
    
    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidateRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(AccessTokenValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public async Task<IActionResult> GetNewTokenFromRefreshToken([FromBody] TokenDto model)
    {
        var tokenDtoResponse = await _userRepository.RefreshAccessToken(model);
        _response.StatusCode = HttpStatusCode.Created;
        _response.IsSuccess = true;
        _response.Result = tokenDtoResponse;
        Response.Cookies.Append("refreshToken", tokenDtoResponse!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });

        Response.Cookies.Append("accessToken", tokenDtoResponse.AccessToken!, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });
        return StatusCode((int)HttpStatusCode.Created, _response);
    }
    
    [HttpPost("revoke")]
    [ServiceFilter(typeof(ValidateRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(AccessTokenValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] TokenDto model)
    {
        if (ModelState.IsValid)
        {
            await _userRepository.RevokeRefreshToken(model);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            Response.Cookies.Append("refreshToken", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None, 
                Secure = _secureCookies
            });

            Response.Cookies.Append("accessToken", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None, 
                Secure = _secureCookies
            });
            return Ok(_response);
                
        }
        _response.IsSuccess = false;
        _response.Result = "Invalid Input";
        Response.Cookies.Append("refreshToken", "", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });

        Response.Cookies.Append("accessToken", "", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });
        return BadRequest(_response);
    }

    [HttpGet("resetPassword", Name="forgotPassword")]
    [ServiceFilter(typeof(EmailExistsFilterAttribute))]
    [ServiceFilter(typeof(UserExistsFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ForgotPassword([FromQuery] string email)
    {
        var token = _userRepository.GetResetToken(email);
        var configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        var sendGridApiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
        Email.DefaultRenderer = new RazorRenderer();
        var templatePath = Path.Combine(_env.ContentRootPath, "Templates/ResetPasswordEmail.cshtml");
        var fluentEmail = Email.From(_emailSender)
            .To(email)
            .Subject(_forgotPasswordSubject)
            .Tag("Reset Password")
            .UsingTemplateFromFile(templatePath, new ForgotPasswordEmail
            {
                ResetLink = "https://bcit.miniaturepug.info/comp/4537/project/m1ResetPassword/resetPassword.html",
                ResetToken = token.Result,
                Title = _forgotPasswordSubject
            });
        var sendGridSender = new SendGridSender(sendGridApiKey);
        var response = sendGridSender.Send(fluentEmail);
        if (response.Successful)
        {
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = "Email sent.";
            return Ok(_response);
        }
        else
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages = (List<string>)response.ErrorMessages;
            return StatusCode((int)HttpStatusCode.InternalServerError, _response);
        }
    }

    [HttpPatch("resetPassword", Name="forgotPassword")]
    [ServiceFilter(typeof(EmailExistsFilterAttribute))]
    [ServiceFilter(typeof(MatchPasswordsFilterAttribute))]
    [ServiceFilter(typeof(UserExistsFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email, [FromBody] ForgotPasswordDto model)
    {
        var result = await _userRepository.UpdatePassword(email, model);
        if (result.IsSuccess)
        {
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = "Password Updated Successfully";
            return Ok(_response);
        }
        else
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.AddRange(result.ErrorMessages.Select(error => error.Description));
            return StatusCode((int)HttpStatusCode.InternalServerError, _response);
        }
    }
}
