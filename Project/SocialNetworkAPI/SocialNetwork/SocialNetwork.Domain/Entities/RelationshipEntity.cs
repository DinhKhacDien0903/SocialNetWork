namespace SocialNetwork.Domain.Entities
{
    public  class RelationshipEntity
    {
        [Key, Column(Order = 0)]
        public Guid UserID { get; set; }

        [Key, Column(Order = 1)]
        public Guid FriendID { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        [ForeignKey("UserID")]
        public UserEntity User { get; set; } = new UserEntity();

        [ForeignKey("FriendID")]
        public UserEntity Friend { get; set; } = new UserEntity();
    }
}
