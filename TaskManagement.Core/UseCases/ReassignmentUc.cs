using Hangfire;
using Microsoft.Extensions.Logging;
using System.Net;
using TaskManagement.Core.Abstractions;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.UseCases;

public class ReassignmentUc(ILogger<ReassignmentUc> logger, ITaskService taskService) : IReassignmentUc
{
    public Task<ResultDto> StartReassignment(string cronExpression)
    {
        try
        {
            RecurringJob.RemoveIfExists(nameof(StartReassignment));
            RecurringJob.AddOrUpdate(
                nameof(StartReassignment),
                () => taskService.ReassignTasksAsync(),
                cronExpression
            );

            logger.LogInformation("Recurring reassignment job scheduled with cron: {Cron}", cronExpression);

            return Task.FromResult(new ResultDto
            {
                IsSuccess = true,
                HttpStatusCode = (int)HttpStatusCode.OK
            });

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while scheduling reassignment job.");

            return Task.FromResult(new ResultDto
            {
                IsSuccess = false,
                HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                MessageError = ex.Message
            });
        }
    }
}
