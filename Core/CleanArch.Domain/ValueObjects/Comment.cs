using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.ValueObjects;

public record Comment
{
    public const int MaxLength = 300;

    private Comment(string value) => Value = value;

    public string Value { get; }

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
