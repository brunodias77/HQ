using HQ.Domain.Abstraction;

namespace HQ.Domain.Entities;

public class Comment : Entity
{
    public string Content { get; set; }
    public DateTime CommentedAt { get; set; }
    public Guid PostID { get; set; } // Foreign Key
    public Guid UserID { get; set; } // Foreign Key

    // Relationships
    public Post Post { get; set; } // Reference to Post
    public User User { get; set; }
}