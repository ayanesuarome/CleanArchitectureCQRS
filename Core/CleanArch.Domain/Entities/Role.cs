using CleanArch.Domain.Utilities;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Domain.Entities;

public sealed class Role : IdentityRole<int>
{
    public Role(string name)
        : base(name)
    {
        Ensure.NotNull(name, "The name is required.", nameof(name));
    }

    public Role(int id, string name)
        : base(name)
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

    public ICollection<Permission> Permissions { get; set; }
    public ICollection<User> Users { get; set; }
}
