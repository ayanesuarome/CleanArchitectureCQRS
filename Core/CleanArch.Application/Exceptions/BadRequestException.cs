using FluentValidation.Results;

namespace CleanArch.Application.Exceptions;

public class BadRequestException : Exception
{
    public List<string> ValidationErrors { get; set; } = [];

    public BadRequestException(string message)
        : base(message) { }

    public BadRequestException(string message, ValidationResult validation)
        : base(message)
    {
        ValidationErrors.AddRange(validation.Errors.Select(x => x.ErrorMessage));
    }
}
