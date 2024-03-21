using CleanArch.Application.Models.Errors;

namespace CleanArch.Application.Models;

internal interface IErrorResult
{
    string Message { get; }
    IReadOnlyCollection<Error> Errors { get; }
}
