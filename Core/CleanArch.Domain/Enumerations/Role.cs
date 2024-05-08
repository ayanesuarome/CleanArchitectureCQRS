using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives;

namespace CleanArch.Domain.Enumerations;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, "Registered");
    public static readonly Role Employee = new(2, "Employee");
    public static readonly Role Administrator = new(3, "Administrator");

    public Role(int id, string name)
        : base(id, name)
    {
    }

    public ICollection<Permission> Permissions { get; set; }
    public ICollection<User> Users { get; set; }
}
