
using TaskManagement.Core.Models;

namespace TaskManagement.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task AddAsync(UserItem user);
    Task<bool> ExistsByNameAsync(string name);
    Task<List<UserItem>> GetAllAsync();
    Task SaveChangesAsync();
}