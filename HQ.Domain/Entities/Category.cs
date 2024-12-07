using HQ.Domain.Abstraction;

namespace HQ.Domain.Entities;

public class Category : Entity
{
    public string Name { get; set; }

    // Relationships
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}