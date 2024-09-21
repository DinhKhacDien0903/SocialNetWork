using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Web.Models
{
    public class TestJwtDbContext : DbContext
    {
        public TestJwtDbContext(DbContextOptions<TestJwtDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
