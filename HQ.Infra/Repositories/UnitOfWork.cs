using HQ.Domain.Repositories;
using HQ.Infra.Data;

namespace HQ.Infra.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}