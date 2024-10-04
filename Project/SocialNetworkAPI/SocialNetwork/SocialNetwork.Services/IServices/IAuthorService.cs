using Microsoft.AspNetCore.Identity;
using SocialNetwork.DTOs.Authorize;
using System.Security.Claims;

namespace SocialNetwork.Services.IServices
{
    public interface IAuthorService
    {
        Task<TokenModel> LoginAsync(LoginRequest loginRequest);

        Task<TokenModel> GenerateJwtToken(UserEntity user);

        Task<bool> ValidateToken(TokenModel tokenModel);

        Task<UserEntity> GetUserByRefreshToken(string token);

        Task<IdentityResult> SignUpAsync(SingUpRequest singUpRequest);

        void SaveTokenToCookieHttpOnly(string name, string token, int expiresMinutes);

        ClaimsPrincipal ValidateAccessToken(string accessToken);

        void RemoveTokenFromCookie(string name);

       
    }
}
