using SocialNetwork.DTOs.Request;

namespace SocialNetwork.DataAccess.Repositories
{
    public class UserRepository :BaseRepository<UserEntity>, IUserRepository
    {
        public readonly SocialNetworkdDataContext _context;

        public UserRepository(SocialNetworkdDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<UserEntity?> GetLoginAsync(LoginRequest loginRequest)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginRequest.UserName && u.PasswordHash == loginRequest.PasswordHash);
        }
    }
}
