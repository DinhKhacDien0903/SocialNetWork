namespace SocialNetwork.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIDAsync(id);

            if (user == null)
            {
                return false;
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangeAsync();

            return true;
        }
 
        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIDAsync(id);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
