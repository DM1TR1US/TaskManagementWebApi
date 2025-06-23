
using TaskManagement.Core.Models;

namespace TaskManagement.Core.Interfaces;

public interface ITaskService
{
    Task<TaskItem> CreateTaskAsync(string title);
    Task ReassignTasksAsync();
}
