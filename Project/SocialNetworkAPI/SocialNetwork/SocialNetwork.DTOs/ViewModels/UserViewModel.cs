namespace SocialNetwork.DTOs.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? AvatarUrl { get; set; }

        public bool? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
