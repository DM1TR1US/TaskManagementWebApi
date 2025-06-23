

using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Abstractions.Repositories;
using TaskManagement.Core.Models;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Dao.Sql;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.TaskItems.AddAsync(task);
    }

    public async Task<bool> ExistsByTitleAsync(string title)
    {
        return await _context.TaskItems.AnyAsync(t => t.Title == title);
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _context.TaskItems.Include(t => t.AssignedUser).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
