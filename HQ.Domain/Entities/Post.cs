using HQ.Domain.Abstraction;

namespace HQ.Domain.Entities;

public class Post : Entity
{
    // Relationships
    public Guid Id { get; set; } // Primary Key
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishedAt { get; set; }
    public Guid UserId { get; set; } // Foreign Key
    public Guid CategoryId { get; set; } // Foreign Key

    // Relationships
    public User User { get; set; }
    public Category Category { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>(); // Alterado
}