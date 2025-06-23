
using TaskManagement.Core.Dto;

namespace TaskManagement.Core.Abstractions;

public interface IReassignmentUc
{
    public Task<ResultDto> StartReassignment(string cronExpression);
}
