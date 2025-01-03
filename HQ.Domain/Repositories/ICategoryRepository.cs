using HQ.Domain.Entities;

namespace HQ.Domain.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<Category?> GetByNameAsync(string name);

}