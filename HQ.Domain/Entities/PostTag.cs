namespace HQ.Domain.Entities;

public class PostTag
{
    public Guid PostId { get; set; } // Foreign Key
    public Guid TagId { get; set; } // Foreign Key

    // Relationships
    public Post Post { get; set; } // Reference to Post
    public Tag Tag { get; set; } // Reference to Tag
}