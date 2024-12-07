namespace HQ.Domain.Entities;

public class PostTag
{
    public Guid PostID { get; set; } // Foreign Key
    public Guid TagID { get; set; } // Foreign Key

    // Relationships
    public Post Post { get; set; } // Reference to Post
    public Tag Tag { get; set; } // Reference to Tag
}