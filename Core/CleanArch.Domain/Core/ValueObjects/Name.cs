using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using Newtonsoft.Json;

namespace CleanArch.Domain.Core.ValueObjects;

/// <summary>
/// Represents the name value object.
/// </summary>
public sealed record Name
{
    /// <summary>
    /// The name maximum length.
    /// </summary>
    public const int MaxLength = 70;

    [JsonConstructor]
    private Name(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(Name value) => value.Value;

    public override string ToString() => Value;

    /// <summary>
    /// Creates a new <see cref="Name"/> instance based on the specified value.
    /// </summary>
    /// <param name="name">The name value.</param>
    /// <returns>The result of the name creation process containing the name or an error.</returns>
    public static Result<Name> Create(string name) =>
        Result.Create(name, DomainErrors.Name.NullOrEmpty)
            .Ensure(n => !string.IsNullOrWhiteSpace(n), DomainErrors.Name.NullOrEmpty)
            .Ensure(n => n.Length <= MaxLength, DomainErrors.Name.LongerThanAllowed)
            .Map(n => new Name(n));
}
