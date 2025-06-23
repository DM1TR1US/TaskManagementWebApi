
namespace TaskManagement.Core.Interfaces;

public interface IDbTransactionService
{
    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
