using CleanArch.Domain.Primitives;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Enumerations;

public sealed class Role : Enumeration<Role>
{
    public Role(int id, string name)
        : base(id, name)
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));
    }

    public static readonly Role Registered = new(1, "Registered");
    public static readonly Role Employee = new(2, "Employee");
    public static readonly Role Administrator = new(3, "Administrator");
}
