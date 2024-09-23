using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.DTOs.Authorize;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SocialNetwork.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;

        public AuthorService(
            IUserRepository userRepository,
            IMapper mapper, IOptionsMonitor<JwtConfig> config, 
            IRefreshTokenService refreshTokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtConfig = config.CurrentValue;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<UserViewModel> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetLoginAsync(loginRequest);

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<TokenModel> GenerateJwtToken(UserViewModel user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
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

            var accessToken = tokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshTokenEntity
            {
                RefreshTokenID = Guid.NewGuid(),
                UserID = user.UserID,
                Token = refreshToken,
                ExpiredAt = DateTime.UtcNow.AddSeconds(20),
                JwtID = token.Id,
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenService.CreateRefreshTokenAsync(refreshTokenEntity);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public async Task<bool> ValidateToken(TokenModel tokenModel)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var screteKeyBytes = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);

            var tokenValidateParamater = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(screteKeyBytes),
                ClockSkew = TimeSpan.Zero,
            };

            var tokenInVerification = tokenHandler.ValidateToken(tokenModel.AccessToken, tokenValidateParamater, out var validatedToken);

            if (!ValidateHeaderAndPayload(tokenInVerification))
            {
                return false;
            }

            var refreshToken = await _refreshTokenService.GetRefreshTokeByTokenAsync(tokenModel.RefreshToken);

            var jwtID = tokenInVerification.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);

            if (!ValidateTokenInDatabase(refreshToken, jwtID?.Value ?? string.Empty))
            {
                return false;
            }

            return true;
        }

        private bool ValidateHeaderAndPayload(ClaimsPrincipal tokenInVerification)
        {

            if (tokenInVerification?.Identity is System.IdentityModel.Tokens.Jwt.JwtSecurityToken jwtSecurityToken)
            {
                return jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.CurrentCultureIgnoreCase);
            }

            var utcExpire = long.Parse(tokenInVerification?.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value??string.Empty);

            var expireDate = ConvertUnixTimeStampToDateTime(utcExpire);

            if (expireDate > DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }

        private bool ValidateTokenInDatabase(RefreshTokenEntity refreshToken, string jwtID)
        {

            if (refreshToken == null)
            {
                return false;
            }

            if (refreshToken.IsUsed)
            {
                return false;
            }

            if (jwtID != refreshToken.JwtID)
            {
                return false;
            }

            return true;
        }

        public async Task<UserViewModel> GetUserByRefreshToken(string token)
        {
            var refreshToken = await _refreshTokenService.GetRefreshTokeByTokenAsync(token);

            var user = await _userRepository.GetByIDAsync(refreshToken.UserID);

            return _mapper.Map<UserViewModel>(user);
        }

        private DateTime ConvertUnixTimeStampToDateTime(long utcExpire)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpire).ToUniversalTime();

            return dateTimeInterval;
        }

    }
}
