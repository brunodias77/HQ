using HQ.Domain.Entities;
using HQ.Domain.Security.PasswordEncripter;

namespace HQ.Domain.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<bool> ExistActiveUserWithEmail(string email);

}