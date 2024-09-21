using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SocialNetwork.Web.Models
{
    public class Helper
    {
        public string GenerateToken(User user, Jwt jwtConfig)
        {
            var jwthandlerToken = new JwtSecurityTokenHandler();
            var secretKeyByte = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);

            //var tokenDescription = new SecurityTokenDescription
            return "token";
        }
    }
}
