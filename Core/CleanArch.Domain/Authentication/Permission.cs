using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Utilities;

namespace CleanArch.Domain.Authentication;

public sealed class Permission : Entity<PermissionId>
{
    public Permission(int id, string name, string description)
        : base(new PermissionId(id))
    {
        Ensure.NotNull(name, "The name is required");
        Ensure.NotNull(description, "The name is required");

        Name = name;
        Description = description;
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

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; private init; }
}
