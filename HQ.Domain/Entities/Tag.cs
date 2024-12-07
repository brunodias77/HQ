using HQ.Domain.Abstraction;

namespace HQ.Domain.Entities;

public class Tag : Entity
{
    public string Name { get; set; }

    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>(); 
}