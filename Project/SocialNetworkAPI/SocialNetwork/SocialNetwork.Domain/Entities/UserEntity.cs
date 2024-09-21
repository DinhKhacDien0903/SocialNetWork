namespace SocialNetwork.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        [Key]
        public Guid UserID { get; set; }

        [Required]
        [StringLength(30)]
        public string? UserName { get; set; }

        [Required]
        [StringLength(64)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(256)]
        public string? Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
