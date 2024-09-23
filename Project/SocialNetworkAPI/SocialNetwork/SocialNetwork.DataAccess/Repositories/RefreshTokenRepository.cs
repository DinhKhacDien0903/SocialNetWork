namespace SocialNetwork.DataAccess.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshTokenEntity>, IRefreshTokenRepository
    {
        private readonly SocialNetworkdDataContext _context;
        public RefreshTokenRepository(SocialNetworkdDataContext context) : base(context)
        {
            _context = context; 
        }
    }
}
