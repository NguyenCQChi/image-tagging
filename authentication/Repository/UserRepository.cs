using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authentication.Data;
using authentication.Models;
using authentication.Models.Dto;
using authentication.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace authentication.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly string _secret;
    private readonly string _issuer;
    private readonly List<string> _audience;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(ApplicationDbContext db, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _mapper = mapper;
        _secret = configuration.GetValue<string>("ApiSettings:Secret")!;
        _issuer = configuration.GetValue<string>("ApiSettings:Issuer")!;
        _audience = configuration.GetSection("ApiSettings:Audience").Get<List<string>>()!;
        _userManager = userManager;
    }
    
    public bool IsUniqueUser(string username)
    {
        var user = _db.ApplicationUsers!.FirstOrDefault(x => x.UserName == username);
        return user == null;
    }
    
    public bool IsUniqueEmail(string email)
    {
        var eMail = _db.ApplicationUsers!.FirstOrDefault(x => x.NormalizedEmail == email.ToUpper());
        return eMail == null;
    }
    
    public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser applicationUser = new()
        {
            UserName = registrationRequestDto.UserName,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email?.ToUpper(),
            Name = registrationRequestDto.Name
        };

        try
        {
            var result = await _userManager.CreateAsync(applicationUser, registrationRequestDto.Password!);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, registrationRequestDto.Role!);
                var userToReturn =
                    _db.ApplicationUsers!.FirstOrDefault(u => u.UserName == registrationRequestDto.UserName);
                return _mapper.Map<UserDto>(userToReturn);
            }
        }
        catch (Exception e)
        {
            // ignored
        }
        return new UserDto();
    }

    public async Task<TokenDto?> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _userManager.FindByNameAsync(loginRequestDto.UserName);
        await _userManager.CheckPasswordAsync(user!, loginRequestDto.Password!);

        var jwtTokenId = $"JTI{Guid.NewGuid()}";
        var accessToken = await GetAccessToken(user!,jwtTokenId);
        var refreshToken = await CreateNewRefreshToken(user!.Id, jwtTokenId);
        TokenDto tokenDto = new()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        return tokenDto;
    }
    
    public async Task<string> GetAccessToken(ApplicationUser user, string jwtTokenId)
    {
        //if user was found generate JWT Token
        var roles = await _userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        
        var audienceClaims = _audience.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)).ToList();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault()!),
                new Claim(JwtRegisteredClaimNames.Jti, jwtTokenId),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Aud, user.Id),
            }.Union(audienceClaims)),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = _issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenStr = tokenHandler.WriteToken(token);
        return tokenStr;
    }
    
    public bool GetAccessTokenData(string accessToken, string expectedUserId, string expectedTokenId)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(accessToken);
            var jwtTokenId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti)!.Value;
            var userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)!.Value;
            return userId==expectedUserId && jwtTokenId== expectedTokenId;
        }
        catch
        {
            return false;
        }
    }
    
    public async Task<string> CreateNewRefreshToken(string userId, string tokenId)
    {
        RefreshToken refreshToken = new()
        {
            IsValid = true,
            UserId = userId,
            JwtTokenId = tokenId,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            Refresh_Token = Guid.NewGuid() + "-" + Guid.NewGuid(),
        };

        await _db.RefreshTokens!.AddAsync(refreshToken);
        await _db.SaveChangesAsync();
        return refreshToken.Refresh_Token;
    }

    public async Task<TokenDto?> RefreshAccessToken(TokenDto tokenDto)
    {
            var existingRefreshToken = await _db.RefreshTokens!.FirstOrDefaultAsync(u => u.Refresh_Token == tokenDto.RefreshToken);
            var applicationUser = _db.ApplicationUsers!.FirstOrDefault(u => u.Id == existingRefreshToken!.UserId);
            var newAccessToken = await GetAccessToken(applicationUser!, existingRefreshToken!.JwtTokenId);

            return new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = tokenDto.RefreshToken,
            };
    }
    
    public async Task MarkAllTokenInChainAsInvalid(string userId, string tokenId)
    {
        var result = await _db.RefreshTokens!.Where(u => u.UserId == userId && u.JwtTokenId == tokenId)
            .ExecuteUpdateAsync(u => u.SetProperty(refreshToken => refreshToken.IsValid, false));
    }
    
    public Task MarkTokenAsInvalid(RefreshToken refreshToken)
    {
        refreshToken.IsValid = false;
        return _db.SaveChangesAsync();
    }

    public async Task<string> GetResetToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user!);
        return token;
    }
    
    public async Task<UserDto> GetUserInformation(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = _mapper.Map<List<UserDto>>(users);
        return userDtos;
    }

    public async Task<(bool IsSuccess, List<IdentityError> ErrorMessages)> UpdatePassword(string email, ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var result = await _userManager.ResetPasswordAsync(user, forgotPasswordDto.Token, forgotPasswordDto.Password);

        return (result.Succeeded, result.Errors.ToList());
    }

    public async Task RevokeRefreshToken(TokenDto tokenDto)
    {
        var existingRefreshToken = await _db.RefreshTokens!.FirstOrDefaultAsync(u => u.Refresh_Token == tokenDto.RefreshToken);
        await MarkAllTokenInChainAsInvalid(existingRefreshToken!.UserId,existingRefreshToken.JwtTokenId);
    }
}