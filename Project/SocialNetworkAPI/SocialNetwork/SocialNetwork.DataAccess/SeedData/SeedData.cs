using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.DataAccess.SeedData
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<UserEntity> userManager)
        {
            var context = serviceProvider.GetRequiredService<SocialNetworkdDataContext>();


            if (!userManager.Users.Any())
            {
                var users = new List<UserEntity>();

                for (int i = 1; i <= 10; i++)
                {
                    var user = new UserEntity
                    {
                        UserName = $"user{i}@test.com",
                        Email = $"user{i}@test.com",
                        FirstName = $"First{i}",
                        LastName = $"Last{i}",
                        IsActive = i <= 8, // 8 users online, 2 users offline
                        CreatedAt = DateTime.UtcNow.AddDays(-i),
                        LastLogin = i <= 8 ? DateTime.UtcNow : (DateTime?)null, // Last login for online users
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, "P@ssw0rd!");

                    if (result.Succeeded)
                    {
                        users.Add(user);
                    }
                }

                await context.SaveChangesAsync();

                // Seed messages for each user
                if (!context.Set<MessagesEntity>().Any())
                {
                    var messages = new List<MessagesEntity>();

                    foreach (var user in users)
                    {
                        var receiver = users.FirstOrDefault(u => u.Id != user.Id); // Find another user to send message to

                        if (receiver != null)
                        {
                            messages.Add(new MessagesEntity
                            {
                                MessageID = Guid.NewGuid(),
                                Content = $"Hello from {user.UserName}",
                                SenderID = user.Id,
                                ReciverID = receiver.Id,
                                IsDeleted = false,
                                CreatedAt = DateTime.UtcNow.AddMinutes(-10)
                            });

                            messages.Add(new MessagesEntity
                            {
                                MessageID = Guid.NewGuid(),
                                Content = $"Reply from {receiver.UserName}",
                                SenderID = receiver.Id,
                                ReciverID = user.Id,
                                IsDeleted = false,
                                CreatedAt = DateTime.UtcNow.AddMinutes(-5)
                            });
                        }
                    }

                    context.AddRange(messages);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
