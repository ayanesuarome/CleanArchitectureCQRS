using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.ValueObjects;

/// <summary>
/// Represents the last name value object.
/// </summary>
public sealed record LastName
{
    /// <summary>
    /// The last name maximum length.
    /// </summary>
    public const int MaxLength = 100;

    private LastName(string value) => Value = value;

    public string Value { get; }

    public override string ToString() => Value;

    /// <summary>
    /// Creates a new <see cref="LastName"/> instance based on the specified value.
    /// </summary>
    /// <param name="lastName">The last name value.</param>
    /// <returns>The result of the last name creation process containing the name or an error.</returns>
    public static Result<LastName> Create(string lastName) =>
        Result.Create(lastName, DomainErrors.LastName.NullOrEmpty)
            .Ensure(l => !string.IsNullOrWhiteSpace(l), DomainErrors.LastName.NullOrEmpty)
            .Ensure(l => l.Length <= MaxLength, DomainErrors.LastName.LongerThanAllowed)
            .Map(l => new LastName(l));
}
