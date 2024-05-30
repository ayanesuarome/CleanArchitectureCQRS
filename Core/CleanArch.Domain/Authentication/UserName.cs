using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using Newtonsoft.Json;

namespace CleanArch.Domain.Authentication;

/// <summary>
/// Represents the user name value object to build both firstname and lastname.
/// </summary>
public sealed record UserName
{
    /// <summary>
    /// The name maximum length.
    /// </summary>
    public const int MaxLength = 100;

    [JsonConstructor]
    private UserName(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(UserName value) => value.Value;

    public override string ToString() => Value;

    /// <summary>
    /// Creates a new <see cref="UserName"/> instance based on the specified value.
    /// </summary>
    /// <param name="name">The first name value.</param>
    /// <returns>The result of the first name creation process containing the name or an error.</returns>
    public static Result<UserName> Create(string name) =>
        Result.Create(name, DomainErrors.FirstName.NullOrEmpty)
            .Ensure(n => !string.IsNullOrWhiteSpace(n), DomainErrors.FirstName.NullOrEmpty)
            .Ensure(n => n.Length <= MaxLength, DomainErrors.FirstName.LongerThanAllowed)
            .Map(n => new UserName(n));
}
