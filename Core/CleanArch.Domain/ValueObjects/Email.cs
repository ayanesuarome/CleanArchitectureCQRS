using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using System.Text.RegularExpressions;

namespace CleanArch.Domain.ValueObjects;

/// <summary>
/// Represents the email value object.
/// </summary>
public sealed record Email
{
    /// <summary>
    /// The email maximum length.
    /// </summary>
    public const int MaxLength = 256;

    private const string EmailRegexPattern = 
        @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
            new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    private Email(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(Email value) => value.Value;

    public override string ToString() => Value;

    /// <summary>
    /// Creates a new <see cref="Email"/> instance based on the specified value.
    /// </summary>
    /// <param name="email">The email value.</param>
    /// <returns>The result of the email creation process containing the name or an error.</returns>
    public static Result<Email> Create(string email) =>
        Result.Create(email, DomainErrors.Name.NullOrEmpty)
            .Ensure(e => !string.IsNullOrWhiteSpace(e), DomainErrors.Email.NullOrEmpty)
            .Ensure(e => e.Length <= MaxLength, DomainErrors.Email.LongerThanAllowed)
            .Ensure(e => EmailFormatRegex.Value.IsMatch(e), DomainErrors.Email.InvalidFormat)
            .Map(e => new Email(e));
}
