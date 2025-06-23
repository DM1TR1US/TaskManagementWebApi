using System;
using TaskManagement.Core.Models;

namespace TaskManagement.Core.Abstractions.Repositories;

public interface ITaskRepository
{
    Task<bool> ExistsByTitleAsync(string title);
    Task AddAsync(TaskItem task);
    Task<List<TaskItem>> GetAllAsync();
    Task SaveChangesAsync();
}
