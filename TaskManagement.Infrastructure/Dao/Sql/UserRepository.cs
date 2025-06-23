

using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Abstractions.Repositories;
using TaskManagement.Core.Models;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Dao.Sql;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserItem user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Users.AnyAsync(u => u.Name == name);
    }

    public async Task<List<UserItem>> GetAllAsync()
    {
        return await _context.Users.OrderBy(u => u.CreatedAt).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
