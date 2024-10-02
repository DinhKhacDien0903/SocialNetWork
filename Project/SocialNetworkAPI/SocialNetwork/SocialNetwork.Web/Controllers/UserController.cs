using Microsoft.AspNetCore.Authorization;
using SocialNetwork.DTOs.Authorize;

namespace SocialNetwork.Web.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [Authorize(Roles = ApplicationRoleModel.User)]
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
