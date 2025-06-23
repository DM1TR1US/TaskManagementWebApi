

using Microsoft.Extensions.Logging;
using System.Net;
using TaskManagement.Core.Abstractions;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Models;

namespace TaskManagement.Core.UseCases;

public class TaskCreationUc(ILogger<TaskCreationUc> logger, ITaskService taskService) : ITaskCreationUc
{
    public async Task<ResultDto> Create(string title)
    {
        try
        {
            await taskService.CreateTaskAsync(title);
            logger.LogInformation("Task created: {Title}", title);
            return new ResultDto { IsSuccess = true, HttpStatusCode = (int)HttpStatusCode.OK };
        }
        catch(HttpRequestException ex)
        {
            logger.LogWarning("Http error while creating task: {Message}", ex.Message);
            return new ResultDto { IsSuccess = false, HttpStatusCode = (int)HttpStatusCode.BadRequest, MessageError = ex.Message };
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating task");
            return new ResultDto { IsSuccess = false, HttpStatusCode = (int)HttpStatusCode.InternalServerError, MessageError = ex.Message };
        }
    }
}
