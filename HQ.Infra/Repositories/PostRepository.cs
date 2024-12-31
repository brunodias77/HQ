using HQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HQ.Infra.Repositories;

public class PostRepository : RepositoryBase<Post>
{
    public PostRepository(DbContext context) : base(context)
    {
    }
}