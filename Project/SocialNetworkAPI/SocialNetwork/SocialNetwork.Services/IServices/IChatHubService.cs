namespace SocialNetwork.Services.IServices
{
    public interface IChatHubService
    {
        Task UpdateStatusActiveUser(string userId, bool isActive);

        Task AddMessagePerson(MessageViewModel messageViewModel);
    }
}
