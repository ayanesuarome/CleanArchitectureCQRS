namespace CleanArch.Domain.Primitives.Result;

public interface IValidationResult
{
    public static Error ValidationError => new("ValidationError", "A validation problem occurred.");

    IReadOnlyCollection<Error> Errors { get; }
}
