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

        public async Task<UserEntity?> GetLoginAsync(string userName, string passwordHash)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.PasswordHash == passwordHash);
        }
    }
}
