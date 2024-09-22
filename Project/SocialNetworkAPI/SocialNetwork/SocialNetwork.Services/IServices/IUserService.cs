namespace SocialNetwork.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<UserViewModel> GetUserByIdAsync(Guid id);
        Task<UserViewModel> LoginAsync(string email, string passwordHash);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
