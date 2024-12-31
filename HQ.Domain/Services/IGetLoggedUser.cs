using HQ.Domain.Entities;

namespace HQ.Domain.Services;

public interface IGetLoggedUser
{
    public Task<User> User();
}