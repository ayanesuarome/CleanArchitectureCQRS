using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Utilities;

namespace CleanArch.Domain.Authentication;

public sealed class Roles : Enumeration<Roles>
{
    public Roles(Guid id, int value, string name)
        : base(value, name)
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));
        Ensure.NotNull(name, "The name is required.", nameof(name));

        Id = id;
    }

    // <summary>
    /// Initializes a new instance of the class <see cref="Roles"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Roles()
    {
    }

    public Guid Id { get; init; }

    public ICollection<Permissions> Permissions { get; set; }
    public ICollection<User> Users { get; set; }

    public static readonly Roles Employee = new(Guid.Parse("ffdb391e-8dd5-4910-9766-02b02c8c1e29"), 2, "Employee");
    public static readonly Roles Administrator = new(Guid.Parse("d9fca87d-b460-43a1-8d72-e31b189dc353"), 3, "Administrator");
}
