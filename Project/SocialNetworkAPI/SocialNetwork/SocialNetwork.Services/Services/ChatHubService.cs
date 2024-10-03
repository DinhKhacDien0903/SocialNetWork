

using SocialNetwork.DataAccess.Repositories;

namespace SocialNetwork.Services.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public ChatHubService(
            IUserRepository userRepository,
            IMessageRepository messageRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task AddMessagePerson(MessageViewModel messageViewModel)
        {
            var entity = _mapper.Map<MessagesEntity>(messageViewModel);
            await _messageRepository.AddAsync(entity);
        }

        public async Task UpdateStatusActiveUser(string userId, bool isActive)
        {
            await _userRepository.UpdateStatusActiveUser(userId, isActive);
        }
    }
}
