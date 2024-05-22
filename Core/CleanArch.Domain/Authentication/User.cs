using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Utilities;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Domain.Authentication;

public sealed class User : IdentityUser<Guid>, IAuditableEntity
{
    public User(UserName firstName, UserName lastName)
    {
        Ensure.NotNull(firstName, "The first name is required.", nameof(firstName));
        Ensure.NotNull(lastName, "The last name is required.", nameof(lastName));

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }

    // <summary>
    /// Initializes a new instance of the class <see cref="User"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private User()
    {
    }

    public UserName FirstName { get; private set; }
    public UserName LastName { get; private set; }

    /// <summary>
    /// Gets the user full name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    #region Auditable

    public DateTimeOffset DateCreated { get; }
    public Guid CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public Guid? ModifiedBy { get; }

    #endregion
}
