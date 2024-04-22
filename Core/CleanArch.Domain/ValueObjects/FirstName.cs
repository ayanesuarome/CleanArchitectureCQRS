using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.ValueObjects;

/// <summary>
/// Represents the first name value object.
/// </summary>
public sealed record FirstName
{
    /// <summary>
    /// The first name maximum length.
    /// </summary>
    public const int MaxLength = 100;

    private FirstName(string value) => Value = value;

    public string Value { get; }

    public override string ToString() => Value;

    /// <summary>
    /// Creates a new <see cref="FirstName"/> instance based on the specified value.
    /// </summary>
    /// <param name="firstName">The first name value.</param>
    /// <returns>The result of the first name creation process containing the name or an error.</returns>
    public static Result<FirstName> Create(string firstName) =>
        Result.Create(firstName, DomainErrors.FirstName.NullOrEmpty)
            .Ensure(f => !string.IsNullOrWhiteSpace(f), DomainErrors.FirstName.NullOrEmpty)
            .Ensure(f => f.Length <= MaxLength, DomainErrors.FirstName.LongerThanAllowed)
            .Map(f => new FirstName(f));
}
