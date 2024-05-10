using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Enumerations;

public sealed class Role : Enumeration<Role>
{
    public Role(Guid id, int value, string name)
        : base(value, name)
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));
        Ensure.NotNull(name, "The name is required.", nameof(name));

        Id = id;
    }

    // <summary>
    /// Initializes a new instance of the class <see cref="Role"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Role()
    {
    }

    public Guid Id { get; init; }

    public ICollection<Permission> Permissions { get; set; }
    public ICollection<User> Users { get; set; }

    public static readonly Role Registered = new(Guid.Parse("881c93c8-6b92-407b-b8bf-196855192ec6"), 1, "Registered");
    public static readonly Role Employee = new(Guid.Parse("ffdb391e-8dd5-4910-9766-02b02c8c1e29"), 2, "Employee");
    public static readonly Role Administrator = new(Guid.Parse("d9fca87d-b460-43a1-8d72-e31b189dc353"), 3, "Administrator");
}
