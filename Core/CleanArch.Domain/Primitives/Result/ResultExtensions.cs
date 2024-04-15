namespace CleanArch.Domain.Primitives.Result;

public static class ResultExtensions
{
    //public static T Match<T, R>(this Result<R> result, Func<T> onSuccess, Func<Error, T> onFailure)
    //{
    //    //return result.IsSuccess ? onSuccess() : onFailure((result as ErrorResult).);
    //}

    /// <summary>
    /// Matches the success status of the result to the corresponding functions.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onSuccess">The on-success function.</param>
    /// <param name="onFailure">The on-failure function.</param>
    /// <returns>
    /// The result of the on-success function if the result is a success result, otherwise the result of the failure result.
    /// </returns>
    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Data) : onFailure(result.Error);
    }

    /// <summary>
    /// Matches the success status of the result to the corresponding functions.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="onSuccess">The on-success function.</param>
    /// <param name="onFailure">The on-failure function.</param>
    /// <returns>
    /// The result of the on-success function if the result is a success result, otherwise the result of the failure result.
    /// </returns>
    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }
}
