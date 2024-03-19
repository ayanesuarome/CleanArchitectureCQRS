using CleanArch.Application.Models;
using CleanArch.Application.Models.Errors;

namespace CleanArch.Application.Extensions;

public static class ResultExtensions
{
    public static T Match<T, R>(this Result<R> result, Func<T> onSuccess, Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }
}
