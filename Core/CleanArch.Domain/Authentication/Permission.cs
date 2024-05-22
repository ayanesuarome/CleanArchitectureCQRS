using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Utilities;

namespace CleanArch.Domain.Authentication;

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
