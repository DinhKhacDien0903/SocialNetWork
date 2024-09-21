using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Web.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
