using SocialNetwork.DTOs.Authorize;

namespace SocialNetwork.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        Task<UserViewModel> GetUserByIdAsync(Guid id);

        Task<bool> DeleteUserAsync(Guid id);
       
    }
}
