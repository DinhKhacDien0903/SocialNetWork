using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Web.Models
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}
