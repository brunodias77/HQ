using HQ.Domain.Entities;
using HQ.Domain.Repositories;
using HQ.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace HQ.Infra.Repositories;

public class PostRepository : RepositoryBase<Post>, IPostRespository
{
    private readonly ApplicationDbContext _context;

    public PostRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>?> GetAllPostsWithUsersAndCategories()
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Category) 
            .ToListAsync();
    }
}