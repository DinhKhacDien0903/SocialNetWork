namespace SocialNetwork.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        Task<UserViewModel> GetUserByIdAsync(Guid id);

        Task<bool> DeleteUserAsync(Guid id);

        string HashPassWord(string password);

        bool VerifyPassword(string hashedPassword, string providePassword);
    }
}
