namespace SocialNetwork.Domain.Entities
{
    public class MessagesEntity:BaseEntity
    {
        [Key]
        public Guid MessageID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Guid SenderID { get; set; }

        [Required]
        public Guid ReciverID { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey("SenderID")]
        public UserEntity Sender { get; set; } = new UserEntity(); 

        [ForeignKey("ReciverID")]
        public UserEntity Receiver { get; set; } = new UserEntity();
    }
}
