using HQ.Domain.Abstraction;

namespace HQ.Domain.Entities;

public class Role : Entity
{
    public string Name { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}