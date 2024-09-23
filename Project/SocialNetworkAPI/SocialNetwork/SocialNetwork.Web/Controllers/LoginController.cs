namespace SocialNetwork.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userServices;
        public LoginController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _userServices.LoginAsync(loginRequest);
            if (user == null)
            {
                return NotFound(new BaseResponse
                {
                    Status = 404,
                    Message = "Not Found User In Server"
                });
            }

            string token = _userServices.GenerateJwtToken(user);

            return Ok(new BaseResponse
            {
                Status = 200,
                Message = "Login success",
                Data = token
            });
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userServices.GetAllUsersAsync();
            if (users == null)
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
                Message = "Get all success success",
                Data = users
            });
        }
    }
}
