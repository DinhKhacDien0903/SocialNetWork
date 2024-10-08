﻿namespace SocialNetwork.Domain.Entities
{
    public class RefreshTokenEntity : BaseEntity
    {
        [Key]
        public Guid RefreshTokenID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        [StringLength(256)]
        public string Token { get; set; }

        [Required]
        [StringLength(256)]
        public string JwtID { get; set; }

        [Required]
        public DateTime ExpiredAt { get; set; }

        [ForeignKey("UserID")]
        public UserEntity User { get; set; } = new UserEntity();
    }
}
