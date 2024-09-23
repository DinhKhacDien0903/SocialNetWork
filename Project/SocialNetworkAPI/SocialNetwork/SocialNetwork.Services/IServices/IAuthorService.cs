using SocialNetwork.DTOs.Authorize;

namespace SocialNetwork.Services.IServices
{
    public interface IAuthorService
    {
        Task<UserViewModel> LoginAsync(LoginRequest loginRequest);

        Task<TokenModel> GenerateJwtToken(UserViewModel user);

        Task<bool> ValidateToken(TokenModel tokenModel);

        Task<UserViewModel> GetUserByRefreshToken(string token);
    }
}
