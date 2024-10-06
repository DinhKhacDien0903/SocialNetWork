using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SocialNetwork.DataAccess.DataContext
{
    public class SocialNetworkdDataContext : IdentityDbContext<UserEntity>
    {
        public SocialNetworkdDataContext(DbContextOptions<SocialNetworkdDataContext> options) : base(options)
        {
        }

        //public DbSet<UserEntity> Users { get; set; }
        //public DbSet<UserInforEntity> UserInfors { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        //public DbSet<RoleEntity> Roles { get; set; }
        //public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<RelationshipEntity> Relationships { get; set; }
        public DbSet<RequestFriendEntity> RequestFriends { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<ImagesOfPostEntity> ImagesOfPosts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<MessagesEntity> Messages { get; set; }
        public DbSet<MessageStatusEntity> MessageStatuses { get; set; }
        public DbSet<MessageImageEntity> MessageImages { get; set; }
        public DbSet<GroupChatEntity> GroupChats { get; set; }
        public DbSet<GroupChatMemberEntity> GroupChatMembers { get; set; }
        public DbSet<GroupChatMessageEntity> GroupChatMessages { get; set; }
        public DbSet<GroupChatMessageStatusEntity> GroupChatMessageStatuses { get; set; }
        public DbSet<GroupChatMessageImageEntity> GroupChatMessageImages { get; set; }
        public DbSet<EmotionTypeEntity> EmotionTypes { get; set; }
        public DbSet<ReactionEntity> Reactions { get; set; }
        public DbSet<ReactionPostEntity> ReactionPosts { get; set; }
        public DbSet<ReactionCommentEntity> ReactionComments { get; set; }
        public DbSet<ReactionMessageEntity> ReactionMessages { get; set; }
        public DbSet<ReactionGroupChatMessageEntity> ReactionGroupChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleEntity>()
                .HasKey(ur => new { ur.UserID, ur.RoleID });

            modelBuilder.Entity<RelationshipEntity>()
                .HasKey(r => new { r.UserID, r.FriendID });

            modelBuilder.Entity<GroupChatMemberEntity>()
                .HasKey(gcm => new { gcm.GroupChatID, gcm.UserID });

            modelBuilder.Entity<ReactionPostEntity>()
                .HasKey(rp => new { rp.ReactionID, rp.PostID });

            modelBuilder.Entity<ReactionCommentEntity>()
                .HasKey(rc => new { rc.ReactionID, rc.CommentID });

            modelBuilder.Entity<ReactionMessageEntity>()
                .HasKey(rm => new { rm.ReactionID, rm.MessageID });

            modelBuilder.Entity<ReactionGroupChatMessageEntity>()
                .HasKey(rgcm => new { rgcm.ReactionID, rgcm.GroupChatMessageID });

            modelBuilder.Entity<GroupChatMessageStatusEntity>()
               .HasKey(g => new { g.GroupChatMessageID, g.UserID});

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

        }

    }
}
