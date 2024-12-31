namespace HQ.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
    Task Rollback(); 

}
