using Microsoft.Extensions.Logging;
using System.Net;
using TaskManagement.Core.Abstractions;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.UseCases;

public class UserCreationUc(ILogger<TaskCreationUc> logger, IUserService userService) : IUserCreationUc
{
    public async Task<ResultDto> Create(string name)
    {
        try
        {
            await userService.CreateUserAsync(name);
            logger.LogInformation("User created: {Name}", name);
            return new ResultDto { IsSuccess = true, HttpStatusCode = (int)HttpStatusCode.OK };
        }
        catch (HttpRequestException ex)
        {
            logger.LogWarning("Http error while creating user: {Message}", ex.Message);
            return new ResultDto { IsSuccess = false, HttpStatusCode = (int)HttpStatusCode.BadRequest, MessageError = ex.Message };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating user");
            return new ResultDto { IsSuccess = false, HttpStatusCode = (int)HttpStatusCode.InternalServerError, MessageError = ex.Message };
        }
    }
}
