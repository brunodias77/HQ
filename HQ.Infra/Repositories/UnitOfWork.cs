using HQ.Domain.Repositories;
using HQ.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace HQ.Infra.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
    public async Task Rollback()
    {
        // Descarta as alterações pendentes no contexto
        foreach (var entry in _dbContext.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached; // Remove a entidade do rastreamento
                    break;
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged; // Reverte para o estado original
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged; // Restaura para o estado original
                    break;
            }
        }

        await Task.CompletedTask; // Garante compatibilidade com métodos assíncronos
    }
}