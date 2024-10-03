namespace SocialNetwork.Services.AuttoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            //xuoi

            CreateMap<UserEntity, UserViewModel>();
            CreateMap<MessagesEntity, MessageViewModel>();


            //nguoc lai
            CreateMap<UserViewModel, UserEntity>();
            CreateMap<MessageViewModel, MessagesEntity>();
        }
    }
}
