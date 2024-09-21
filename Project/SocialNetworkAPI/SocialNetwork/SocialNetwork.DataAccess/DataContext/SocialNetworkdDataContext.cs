using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.DataContext
{
    public class SocialNetworkdDataContext : DbContext
    {
        public SocialNetworkdDataContext(DbContextOptions<SocialNetworkdDataContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserInforEntity> UserInfors { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
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
            // UserRoleEntity
            modelBuilder.Entity<UserRoleEntity>()
                .HasKey(ur => new { ur.UserID, ur.RoleID });

            // RelationshipEntity
            modelBuilder.Entity<RelationshipEntity>()
                .HasKey(r => new { r.UserID, r.FriendID });

            // GroupChatMemberEntity
            modelBuilder.Entity<GroupChatMemberEntity>()
                .HasKey(gcm => new { gcm.GroupChatID, gcm.UserID });

            // ReactionPostEntity
            modelBuilder.Entity<ReactionPostEntity>()
                .HasKey(rp => new { rp.ReactionID, rp.PostID });

            // ReactionCommentEntity
            modelBuilder.Entity<ReactionCommentEntity>()
                .HasKey(rc => new { rc.ReactionID, rc.CommentID });

            // ReactionMessageEntity
            modelBuilder.Entity<ReactionMessageEntity>()
                .HasKey(rm => new { rm.ReactionID, rm.MessageID });

            // ReactionGroupChatMessageEntity
            modelBuilder.Entity<ReactionGroupChatMessageEntity>()
                .HasKey(rgcm => new { rgcm.ReactionID, rgcm.GroupChatMessageID });

            modelBuilder.Entity<GroupChatMessageStatusEntity>()
               .HasKey(g => new { g.GroupChatMessageID, g.UserID});

            base.OnModelCreating(modelBuilder);
        }

    }
}
