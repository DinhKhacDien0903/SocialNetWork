﻿namespace SocialNetwork.Services.AuttoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserEntity, UserViewModel>();
        }
    }
}
