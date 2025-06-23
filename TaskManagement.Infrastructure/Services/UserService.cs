using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Abstractions.Repositories;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Models;

namespace TaskManagement.Infrastructure.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserItem> CreateUserAsync(string name)
    {
        if (await userRepository.ExistsByNameAsync(name))
            throw new HttpRequestException("User name already exists.");

        var user = new UserItem
        {
            Name = name,
        };

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();
        return user;
    }
}
