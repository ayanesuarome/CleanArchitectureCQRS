using CleanArch.Domain.Primitives;
using CleanArch.Domain.Utilities;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Domain.Entities;

public sealed class Permission : Entity<PermissionId>
{
    public Permission(int id, string name)
        : base(new PermissionId(id))
    {
        Ensure.NotNull(name, "The name is required", nameof(name));
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the class <see cref="Permission"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Permission()
    {
    }

    public string Name { get; init; }
}
