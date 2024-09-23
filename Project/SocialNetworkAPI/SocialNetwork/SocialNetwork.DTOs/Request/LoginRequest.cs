namespace SocialNetwork.DTOs.Request
{
    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
