using Microsoft.AspNetCore.Identity;
using SocialNetwork.DTOs.Authorize;

namespace SocialNetwork.Services.IServices
{
    public interface IAuthorService
    {
        Task<TokenModel> LoginAsync(LoginRequest loginRequest);

        Task<TokenModel> GenerateJwtToken(UserEntity user);

        Task<bool> ValidateToken(TokenModel tokenModel);

        Task<UserEntity> GetUserByRefreshToken(string token);

        Task<IdentityResult> SignUpAsync(SingUpRequest singUpRequest);
    }
}
