using SocialNetwork.DTOs.Authorize;

namespace SocialNetwork.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthorService _authServices;

        private readonly IRefreshTokenService _refreshTokenService;

        public LoginController(IAuthorService authServices, IRefreshTokenService refreshTokenService)
        {
            _authServices = authServices;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _authServices.LoginAsync(loginRequest);
            if (user == null)
            {
                return NotFound(new BaseResponse
                {
                    Status = 404,
                    Message = "Not Found User In Server"
                });
            }

            var token = await _authServices.GenerateJwtToken(user);

            return Ok(new BaseResponse
            {
                Status = 200,
                Message = "Login success",
                Data = token
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            try
            {
                bool IsValideToken = await _authServices.ValidateToken(tokenModel);

                if(!IsValideToken)
                {
                    return BadRequest(new BaseResponse
                    {
                        Status = 400,
                        Message = "Invalid token"
                    });
                }

                await _refreshTokenService.UpdateRefreshTokenAsync(tokenModel.RefreshToken);

                var user = await _authServices.GetUserByRefreshToken(tokenModel.RefreshToken);

                var newToken = await _authServices.GenerateJwtToken(user);

                return Ok(new BaseResponse
                {
                    Status = 200,
                    Message = "Refresh token success",
                    Data = newToken
                });

            }
            catch
            {
                return Ok(new BaseResponse
                {
                    Status = 404,
                    Message = "Something went wrong"
                });
            }
        }
    }
}
