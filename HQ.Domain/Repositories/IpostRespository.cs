using HQ.Domain.Entities;

namespace HQ.Domain.Repositories;

public interface IPostRespository : IRepositoryBase<Post>
{
    Task<IEnumerable<Post>?> GetAllPostsWithUsersAndCategories();

}