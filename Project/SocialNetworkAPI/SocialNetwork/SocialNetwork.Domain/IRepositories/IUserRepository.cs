using SocialNetwork.DTOs.Request;

namespace SocialNetwork.Domain.IRepositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetByUserNameAsync(string userName);

        Task<UserEntity?> GetLoginAsync(LoginRequest loginRequest);
    }
}
