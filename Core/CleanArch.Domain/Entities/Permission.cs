using CleanArch.Domain.Primitives;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Entities;

public sealed class Permission : Entity<int>
{
    public Permission(string name)
    {
        Ensure.NotNull(name, "The name is required", nameof(name));
        Name = name;
    }

    public Permission(int id, string name)
        : base(id)
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
