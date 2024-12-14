using HQ.Domain.Entities;
using HQ.Domain.Repositories;
using HQ.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace HQ.Infra.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
    }
}