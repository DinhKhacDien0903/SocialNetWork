using Microsoft.AspNetCore.Identity;
using SocialNetwork.DTOs.Request;

namespace SocialNetwork.DataAccess.Repositories
{
    public class UserRepository :BaseRepository<UserEntity>, IUserRepository
    {
        public readonly SocialNetworkdDataContext _context;

        private readonly UserManager<UserEntity> _userManager;
        public UserRepository(SocialNetworkdDataContext context, UserManager<UserEntity> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserEntity?> GetByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<UserEntity?> GetLoginAsync(LoginRequest loginRequest)
        {
            var user =  await _userManager.FindByEmailAsync(loginRequest.Email);
            if(user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return user;
            }
            return null;
        }
    }
}
