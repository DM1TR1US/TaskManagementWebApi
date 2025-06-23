
using TaskManagement.Core.Dto;

namespace TaskManagement.Core.Abstractions;

public interface IUserCreationUc
{
    public Task<ResultDto> Create(string name);
}
