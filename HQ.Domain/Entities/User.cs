using HQ.Domain.Abstraction;

namespace HQ.Domain.Entities;

public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool Active { get; set; }

    // Relationships
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

}