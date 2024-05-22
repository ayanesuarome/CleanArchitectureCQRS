using CleanArch.Domain.Core.Utilities;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Domain.Authentication;

public sealed class Role : IdentityRole<Guid>
{
    public Role(Guid id, string name)
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
