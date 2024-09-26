using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var token = await _authServices.LoginAsync(loginRequest);

                if (token == null)
                {
                    return NotFound(new BaseResponse
                    {
                        Status = 404,
                        Message = "Not Found User In Server"
                    });
                }

                return Ok(new BaseResponse
                {
                    Status = 200,
                    Message = "Login success",
                    Data = token
                });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("singup")]
        public async Task<ActionResult> SingUp(SingUpRequest singUpRequest)
        {
            try
            {
                var result = await _authServices.SignUpAsync(singUpRequest);
                if (result.Succeeded)
                {
                    return Ok(new BaseResponse
                    {
                        Status = 200,
                        Message = "Sing up success",
                        Data = result.Succeeded
                    });
                }

                return BadRequest(new BaseResponse
                {
                    Status = 400,
                    Message = "Invalid token",
                    Data = result.Errors
                });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("RefreshToken")]
        [Authorize]
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

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(TokenModel tokenModel)
        {
            try
            {
                bool IsValideToken = await _authServices.ValidateToken(tokenModel);

                if (!IsValideToken)
                {
                    return BadRequest(new BaseResponse
                    {
                        Status = 400,
                        Message = "Invalid token"
                    });
                }

                await _refreshTokenService.UpdateRefreshTokenAsync(tokenModel.RefreshToken);

                return Ok(new BaseResponse
                {
                    Status = 200,
                    Message = "Logout success",
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
