using CleanArch.Domain.Utilities;
using CleanArch.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Domain.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public ApplicationUser(FirstName firstName, LastName lastName)
        : base()
    {
        Ensure.NotNull(firstName, "The first name is required.", nameof(firstName));
        Ensure.NotNull(lastName, "The last name is required.", nameof(lastName));

        FirstName = firstName;
        LastName = lastName;
    }

    // <summary>
    /// Initializes a new instance of the class <see cref="ApplicationUser"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private ApplicationUser()
    {
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; set; }

    /// <summary>
    /// Gets the user full name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
}
