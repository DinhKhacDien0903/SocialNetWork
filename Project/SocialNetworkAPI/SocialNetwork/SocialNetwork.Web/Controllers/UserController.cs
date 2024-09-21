using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TestJwtDbContext _context;
        private readonly Jwt _jwtConfig = new Jwt();
        public UserController(TestJwtDbContext context, IOptionsMonitor<Jwt> optionsMonitor)
        {
            _context = context;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(loginRequest.Username) && u.Password.Equals(loginRequest.Password));
            if (currentUser == null)
            {
                return NotFound(new ResponeBase
                { 
                    Success = false,
                    Message = "User not register in system!" 
                });
            }
            //generate token
            string token = new Helper().GenerateToken(currentUser, _jwtConfig);


            return Ok( new LoginResponse
            {
                Success = true,
                Message = "Login success!",
                Token = "token",
                Username = currentUser.Username,
                Email = currentUser.Email
            });
        }
    }
}
