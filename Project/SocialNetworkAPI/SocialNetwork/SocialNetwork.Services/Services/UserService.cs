using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig; 

        public UserService(IUserRepository userRepository, IMapper mapper, IOptionsMonitor<JwtConfig> config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtConfig = config.CurrentValue;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIDAsync(id);

            if (user == null)
            {
                return false;
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangeAsync();

            return true;
        }

        public string GenerateJwtToken(UserEntity user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),

                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;

        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIDAsync(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserEntity> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetLoginAsync(loginRequest);
            return user;
        }
    }
}
