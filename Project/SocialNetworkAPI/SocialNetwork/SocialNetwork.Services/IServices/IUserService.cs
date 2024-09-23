﻿using SocialNetwork.DTOs.Authorize;

namespace SocialNetwork.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<UserViewModel> GetUserByIdAsync(Guid id);
        Task<UserEntity> LoginAsync(LoginRequest loginRequest);
        Task<bool> DeleteUserAsync(Guid id);

        Task<TokenModel> GenerateJwtToken(UserEntity user);

        Task<bool> ValidateToken(TokenModel tokenModel);
    }
}
