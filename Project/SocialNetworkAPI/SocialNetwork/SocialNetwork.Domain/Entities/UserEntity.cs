using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Domain.Entities
{
    public class UserEntity : IdentityUser
    {
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }
    }
}
