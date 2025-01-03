using System.Data;

namespace HQ.Domain.Repositories;

public interface IUnitOfWork
{
    Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    Task Commit();
    Task Rollback();
}