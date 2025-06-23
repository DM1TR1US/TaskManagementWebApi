using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Core.Abstractions.Repositories;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Models;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Services;

public class TaskService(ILogger<TaskService> logger, ITaskRepository taskRepository, IUserRepository userRepository, IDbTransactionService dbTransaction) : ITaskService
{

    public async Task<TaskItem> CreateTaskAsync(string title)
    {
        if (await taskRepository.ExistsByTitleAsync(title))
            throw new HttpRequestException("Task title must be unique");

        var task = new TaskItem { Title = title };

        var availableUsers = await userRepository.GetAllAsync();

        if (availableUsers.Any())
        {
            var user = availableUsers.First();

            task.AssignedUserId = user.Id;
            task.AssignedUser = user;
            task.State = TaskState.InProgress;

        }
        else
        {
            task.State = TaskState.Waiting;
        }

        await taskRepository.AddAsync(task);
        await taskRepository.SaveChangesAsync();

        return task;
    }

    public async Task ReassignTasksAsync()
    {
        const int maxRetries = 3;

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                await dbTransaction.BeginAsync();

                var users = await userRepository.GetAllAsync();
                var tasks = await taskRepository.GetAllAsync();
                var allUserIds = users.Select(u => u.Id).ToHashSet();

                foreach (var task in tasks.Where(t => t.State != TaskState.Completed))
                {
                    if (!TryAssignPreferredUser(task, users))
                    {
                        if (task.WasAssignedToAll(allUserIds))
                        {
                            task.CompleteTask();
                        }
                        else
                        {
                            task.State = TaskState.Waiting;
                        }
                    }
                }

                await taskRepository.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                logger.LogInformation("Reassign transaction committed successfully on attempt {Attempt}", attempt);
                break;
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                logger.LogWarning("Reassign attempt {Attempt}/{MaxAttempts} failed. Error: {Error}", attempt, maxRetries, ex.Message);

                if (attempt == maxRetries)
                    throw;

                await Task.Delay(200);
            }
        }
    }

    private bool TryAssignPreferredUser(TaskItem task, List<UserItem> users)
    {
        var assigned = task.AssignmentHistory.Distinct().ToHashSet();
        var candidate = users.FirstOrDefault(u => !assigned.Contains(u.Id) && task.AssignedUserId != u.Id);

        if (candidate != null && task.AssignTo(candidate.Id))
        {
            logger.LogInformation("Task {Title} reassigned (preferred) to {UserId}", task.Title, task.AssignedUserId);
            return true;
        }

        return false;
    }
}
