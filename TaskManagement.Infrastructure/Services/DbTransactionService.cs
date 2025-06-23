
using Microsoft.EntityFrameworkCore.Storage;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Services;

public class DbTransactionService : IDbTransactionService
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public DbTransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task BeginAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync(); ;
    }
}
