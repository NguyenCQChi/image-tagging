using authentication.Models;
using authentication.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;

namespace authentication.Repository.IRepository;

public interface IUserRepository
{
    bool IsUniqueUser(string username);
    
    bool IsUniqueEmail(string email);
    
    Task<TokenDto?> Login(LoginRequestDto loginRequestDto);
    
    Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);
    
    Task<TokenDto?> RefreshAccessToken(TokenDto tokenDto);

    Task RevokeRefreshToken(TokenDto tokenDto);

    Task<string> GetAccessToken(ApplicationUser user, string jwtTokenId);

    bool GetAccessTokenData(string accessToken, string expectedUserId, string expectedTokenId);

    Task<string> CreateNewRefreshToken(string userId, string tokenId);

    Task MarkAllTokenInChainAsInvalid(string userId, string tokenId);

    Task MarkTokenAsInvalid(RefreshToken refreshToken);

    Task<string> GetResetToken(string email);
    
    Task<(bool IsSuccess, List<IdentityError> ErrorMessages)> UpdatePassword(string email, ForgotPasswordDto forgotPasswordDto);

    Task<UserDto> GetUserInformation(string userName);
    
    Task<List<EndpointRequestCountInfo>> GetTotalRequestsPerEndpoint();

    Task<List<UserEndpointInfo>> GetAllUsers();
    
    Task<List<EndpointTypesDto>> GetAllEndpoints();

    Task<ApplicationUser?> FindUser(string userName);

    Task<ApplicationUser?> FindUser(TokenDto model);
    
    Task<ApplicationUser?> FindUserByEmail(string email);

    Task<EndpointType?> FindEndpointType(string endpointName, string requestName);
}