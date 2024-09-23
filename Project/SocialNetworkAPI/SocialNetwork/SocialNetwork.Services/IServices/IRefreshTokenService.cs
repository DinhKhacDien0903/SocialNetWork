namespace SocialNetwork.Services.IServices
{
    public interface IRefreshTokenService
    {
        Task CreateRefreshTokenAsync(RefreshTokenEntity entity);
        Task<RefreshTokenEntity> GetRefreshTokeByTokenAsync(string token);

        Task RefreshTokeByTokenAsync(string token);
    }
}
