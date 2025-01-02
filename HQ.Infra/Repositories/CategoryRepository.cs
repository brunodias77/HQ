using HQ.Domain.Entities;
using HQ.Domain.Repositories;
using HQ.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace HQ.Infra.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;

    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }
}