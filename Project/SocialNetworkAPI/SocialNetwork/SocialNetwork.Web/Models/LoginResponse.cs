namespace SocialNetwork.Web.Models
{
    public class LoginResponse : ResponeBase
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
