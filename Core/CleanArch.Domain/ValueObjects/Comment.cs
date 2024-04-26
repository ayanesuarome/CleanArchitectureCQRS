using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace CleanArch.Domain.ValueObjects;

// EF8 Complex Types  does not support nullable.
// This value object must be used with OwnsOne.
public record Comment
{
    public const int MaxLength = 300;

    private Comment(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(Comment value) => value.Value;

    public override string ToString() => Value.ToString();

    /// <summary>
    /// Creates a new <see cref="Comment"/> instance based on the specified value.
    /// </summary>
    /// <param name="comments">The comments value.</param>
    /// <returns>The result of the comments creation process containing the name or an error.</returns>
    public static Result<Comment> Create(string comments)
    {
        return Result.Create(comments, DomainErrors.Comment.NullOrEmpty)
            .Ensure(c => !string.IsNullOrWhiteSpace(c), DomainErrors.Comment.NullOrEmpty)
            .Ensure(c => c.Length <= MaxLength, DomainErrors.Comment.LongerThanAllowed)
            .Map(c => new Comment(c));
    }
}
