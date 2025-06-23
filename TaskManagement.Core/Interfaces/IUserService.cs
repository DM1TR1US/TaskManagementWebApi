using TaskManagement.Core.Models;

namespace TaskManagement.Core.Interfaces;

public interface IUserService
{
    Task<UserItem> CreateUserAsync(string name);
}
