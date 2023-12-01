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
    private readonly IConfiguration _configuration;
    private readonly ApiResponse _response;
    private readonly string _emailSender;
    private readonly string _forgotPasswordSubject;
    private readonly bool _secureCookies;
    private readonly IWebHostEnvironment _env;

    public UsersController(IUserRepository userRepository, IConfiguration configuration, IWebHostEnvironment env)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        this._response = new ApiResponse();
        _emailSender = configuration.GetValue<string>("EmailSettings:ForgotPasswordSender")!;
        _forgotPasswordSubject = configuration.GetValue<string>("EmailSettings:ForgotPasswordSubject")!;
        _secureCookies = configuration.GetValue<bool>("ApiSettings:SecureCookies")!;
        _env = env;
    }
    
    [SwaggerOperation(Summary = "Register a new user")]
    [HttpPost("register")]
    [ServiceFilter(typeof(RegistrationRoleFilterAttribute))]
    [ServiceFilter(typeof(RegistrationEmailFilterAttribute))]
    [ServiceFilter(typeof(RegistrationUserNameFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {
        await _userRepository.Register(model);
        _response.StatusCode = HttpStatusCode.Created;
        _response.IsSuccess = true;
        try
        {
            var user = await _userRepository.FindUser(model.UserName);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        
        return StatusCode((int)HttpStatusCode.Created, _response);
    }
    
    [SwaggerOperation(
        Summary = "Login with an existing user.",
        Description = "Returns AccessToken and RefreshToken")]
    [HttpPost("login")]
    [ServiceFilter(typeof(LoginUserValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var tokenDto = await _userRepository.Login(model);
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = tokenDto;
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = tokenDto;
        Response.Cookies.Append("X-Refresh-Token", tokenDto!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });

        Response.Cookies.Append("Authorization", "Bearer " + tokenDto.AccessToken!, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });
        try
        {
            var user = await _userRepository.FindUser(model.UserName);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        return Ok(_response);
    }
    
    [SwaggerOperation(
        Summary = "Validate User's Access Token",
        Description = "Using nodejs fetch, make a post request with header 'Authorization': " +
                                    "`Bearer \n${jwtToken}`")]
    [HttpPost("validate")]
    [Authorize]
    [ServiceFilter(typeof(ValidateRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(AccessTokenValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> ValidateTokens([FromBody] TokenDto model)
    {
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = _configuration.GetValue<string>("UserResponseStrings:AuthSuccess")!;
        try
        {
            var user = await _userRepository.FindUser(model);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        return Ok(_response);
    }
    [SwaggerOperation(
        Summary = "Refresh a user's AccessToken with the help of RefreshToken",
        Description = "Returns AccessToken and RefreshToken")]
    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidateRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(AccessTokenValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> GetNewTokenFromRefreshToken([FromBody] TokenDto model)
    {
        var tokenDtoResponse = await _userRepository.RefreshAccessToken(model);
        _response.StatusCode = HttpStatusCode.Created;
        _response.IsSuccess = true;
        _response.Result = tokenDtoResponse;
        Response.Cookies.Append("X-Refresh-Token", tokenDtoResponse!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });

        Response.Cookies.Append("Authorization", "Bearer " + tokenDtoResponse.AccessToken!, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });
        try
        {
            var user = await _userRepository.FindUser(model);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        return StatusCode((int)HttpStatusCode.Created, _response);
    }
    
    [SwaggerOperation(
        Summary = "Revoke user's AccessToken and RefreshToken",
        Description = "Ignore all response codes for this request. We don't want to block the user.")]
    [HttpDelete("revoke")]
    [ServiceFilter(typeof(ValidateRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(AccessTokenValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] TokenDto model)
    {
        if (ModelState.IsValid)
        {
            await _userRepository.RevokeRefreshToken(model);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            Response.Cookies.Append("X-Refresh-Token", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None, 
                Secure = _secureCookies
            });

            Response.Cookies.Append("Authorization", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None, 
                Secure = _secureCookies
            });
            try
            {
                var user = await _userRepository.FindUser(model);
                var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
                HttpContext.Items["user"] = user;
                HttpContext.Items["endpointType"] = endpointType;
            } catch (Exception e)
            {
                // ignored
            }
            return Ok(_response);
        }
        _response.IsSuccess = false;
        _response.Result = _configuration.GetValue<string>("UserResponseStrings:InvalidInput")!;
        Response.Cookies.Append("X-Refresh-Token", "", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });

        Response.Cookies.Append("Authorization", "", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, 
            Secure = _secureCookies
        });
        try
        {
            var user = await _userRepository.FindUser(model);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        return BadRequest(_response);
    }
    
    [SwaggerOperation(
        Summary = "Reset a user's password via email",
        Description = "Check if the email exists in the DB and send them a password reset email.")]
    [HttpGet("resetPassword", Name="forgotPassword")]
    [ServiceFilter(typeof(EmailExistsFilterAttribute))]
    [ServiceFilter(typeof(UserExistsFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email)
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
            _response.Result = _configuration.GetValue<string>("UserResponseStrings:EmailSent")!;
            try
            {
                var user = await _userRepository.FindUserByEmail(email);
                var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
                HttpContext.Items["user"] = user;
                HttpContext.Items["endpointType"] = endpointType;
            } catch (Exception e)
            {
                // ignored
            }
            return Ok(_response);
        }
        else
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages = (List<string>)response.ErrorMessages;
            try
            {
                var user = await _userRepository.FindUserByEmail(email);
                var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
                HttpContext.Items["user"] = user;
                HttpContext.Items["endpointType"] = endpointType;
            } catch (Exception e)
            {
                // ignored
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, _response);
        }
    }
    
    [SwaggerOperation(Summary = "Reset a user's password given email and secret token")]
    [HttpPatch("resetPassword", Name="forgotPassword")]
    [ServiceFilter(typeof(EmailExistsFilterAttribute))]
    [ServiceFilter(typeof(MatchPasswordsFilterAttribute))]
    [ServiceFilter(typeof(UserExistsFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email, [FromBody] ForgotPasswordDto model)
    {
        var result = await _userRepository.UpdatePassword(email, model);
        if (result.IsSuccess)
        {
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _configuration.GetValue<string>("UserResponseStrings:PasswordUpdateSuccess")!;
            try
            {
                var user = await _userRepository.FindUserByEmail(email);
                var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
                HttpContext.Items["user"] = user;
                HttpContext.Items["endpointType"] = endpointType;
            } catch (Exception e)
            {
                // ignored
            }
            return Ok(_response);
        }
        else
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.AddRange(result.ErrorMessages.Select(error => error.Description));
            try
            {
                var user = await _userRepository.FindUserByEmail(email);
                var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
                HttpContext.Items["user"] = user;
                HttpContext.Items["endpointType"] = endpointType;
            } catch (Exception e)
            {
                // ignored
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, _response);
        }
    }
    
    [SwaggerOperation(
        Summary = "Get user information",
        Description = "Using nodejs fetch, make a get request with headers: 'Authorization': " +
                                    "`Bearer \n${jwtToken}` and 'X-Refresh-Token': `<refreshToken>`")]
    [HttpGet("userInformation")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(UserInformationHeaderFilterAttribute))]
    [ServiceFilter(typeof(HeaderRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(HeaderAccessTokenFilterAttribute))]
    [ServiceFilter(typeof(UserNameExistsFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> GetUserInformation([FromQuery] string userName)
    {
        var userInfo = await _userRepository.GetUserInformation(userName);
        _response.IsSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = userInfo;
        try
        {
            Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues);
            Request.Headers.TryGetValue("X-Refresh-Token", out var refreshHeaderValues);
            var authorizationHeaderValue = authorizationHeaderValues.FirstOrDefault()?.Replace("Bearer", "").Trim();
            var refreshHeaderValue = refreshHeaderValues.FirstOrDefault()?.Trim();
            var model = new TokenDto
            {
                AccessToken = authorizationHeaderValue,
                RefreshToken = refreshHeaderValue!
            };
            var user = await _userRepository.FindUser(model);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        return Ok(_response);
    }

    [SwaggerOperation(
        Summary = "Get all users information",
        Description = "Using nodejs fetch, make a get request with headers: 'Authorization': " +
                      "`Bearer \n${jwtToken}` and 'X-Refresh-Token': `<refreshToken>`")]
    [HttpGet("allUserInformation")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(UserInformationHeaderFilterAttribute))]
    [ServiceFilter(typeof(HeaderRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(HeaderAccessTokenFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllUserInformation()
    {
        var userEndpointInfo = await _userRepository.GetAllUsers();
        _response.IsSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = userEndpointInfo;
        try
        {
            Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues);
            Request.Headers.TryGetValue("X-Refresh-Token", out var refreshHeaderValues);
            var authorizationHeaderValue = authorizationHeaderValues.FirstOrDefault()?.Replace("Bearer", "").Trim();
            var refreshHeaderValue = refreshHeaderValues.FirstOrDefault()?.Trim();
            var model = new TokenDto
            {
                AccessToken = authorizationHeaderValue,
                RefreshToken = refreshHeaderValue!
            };
            var user = await _userRepository.FindUser(model);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        return Ok(_response);
    }
    
    [SwaggerOperation(
        Summary = "Get a list of total requests per endpoint",
        Description = "Using nodejs fetch, make a get request with headers: 'Authorization': " +
                      "`Bearer \n${jwtToken}` and 'X-Refresh-Token': `<refreshToken>`")]
    [HttpGet("totalRequestsPerEndpoint")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(UserInformationHeaderFilterAttribute))]
    [ServiceFilter(typeof(HeaderRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(HeaderAccessTokenFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> GetTotalRequestsPerEndpoint()
    {
        var requestsPerEndpoint = await _userRepository.GetTotalRequestsPerEndpoint();
        _response.IsSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = requestsPerEndpoint;
        try
        {
            Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues);
            Request.Headers.TryGetValue("X-Refresh-Token", out var refreshHeaderValues);
            var authorizationHeaderValue = authorizationHeaderValues.FirstOrDefault()?.Replace("Bearer", "").Trim();
            var refreshHeaderValue = refreshHeaderValues.FirstOrDefault()?.Trim();
            var model = new TokenDto
            {
                AccessToken = authorizationHeaderValue,
                RefreshToken = refreshHeaderValue!
            };
            var user = await _userRepository.FindUser(model);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        return Ok(_response);
    }
    
    [SwaggerOperation(
        Summary = "Get a list of endpoints available",
        Description = "Using nodejs fetch, make a get request with headers: 'Authorization': " +
                      "`Bearer \n${jwtToken}` and 'X-Refresh-Token': `<refreshToken>`")]
    [HttpGet("getAllEndpoints")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(UserInformationHeaderFilterAttribute))]
    [ServiceFilter(typeof(HeaderRefreshTokenFilterAttribute))]
    [ServiceFilter(typeof(HeaderAccessTokenFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(ApiResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllEndpoints()
    {
        var endpoints = await _userRepository.GetAllEndpoints();
        _response.IsSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = endpoints;
        try
        {
            Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues);
            Request.Headers.TryGetValue("X-Refresh-Token", out var refreshHeaderValues);
            var authorizationHeaderValue = authorizationHeaderValues.FirstOrDefault()?.Replace("Bearer", "").Trim();
            var refreshHeaderValue = refreshHeaderValues.FirstOrDefault()?.Trim();
            var model = new TokenDto
            {
                AccessToken = authorizationHeaderValue,
                RefreshToken = refreshHeaderValue!
            };
            var user = await _userRepository.FindUser(model);
            var endpointType = await _userRepository.FindEndpointType(Request.Path, Request.Method);
            HttpContext.Items["user"] = user;
            HttpContext.Items["endpointType"] = endpointType;
        } catch (Exception e)
        {
            // ignored
        }
        return Ok(_response);
    }
}
