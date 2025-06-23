
using TaskManagement.Core.Dto;

namespace TaskManagement.Core.Abstractions;

public interface ITaskCreationUc
{
    public Task<ResultDto> Create(string title);
}
