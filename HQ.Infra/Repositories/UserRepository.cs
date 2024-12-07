using HQ.Domain.Entities;
using HQ.Domain.Repositories;
using HQ.Infra.Data;

namespace HQ.Infra.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

}