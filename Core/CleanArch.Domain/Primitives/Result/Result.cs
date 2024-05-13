namespace CleanArch.Domain.Primitives.Result;

/// <summary>
/// Represents a result of some operation, with status information and possibly an error.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the result is a success result.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the result is a failure result.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the error.
    /// </summary>
    public Error Error { get; }

    internal protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Returns a success <see cref="Result"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="Result"/> with the success flag set.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Returns a failure <see cref="Result"/> with the specified error.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>A new instance of <see cref="Result"/> with the specified error and failure flag set.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Returns a success <see cref="Result{T}"/> with the specified value.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="value">The result value.</param>
    /// <returns>A new instance of <see cref="Result{T}"/> with the success flag set.</returns>
    public static Result<T> Success<T>(T value) => new(value);

    /// <summary>
    /// Returns a failure <see cref="Result{T}"/> with the specified error.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="error">The error.</param>
    /// <returns>A new instance of <see cref="Result{T}"/> with the specified error and failure flag set.</returns>
    /// <remarks>
    /// We're purposefully ignoring the nullable assignment here because the API will never allow it to be accessed.
    /// The value is accessed through a method that will throw an exception if the result is a failure result.
    public static Result<T> Failure<T>(Error error) => new(error);

    /// <summary>
    /// Creates a new <see cref="Result{T}"/> with the specified nullable value and the specified error.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="value">The result value.</param>
    /// <param name="error">The error in case the value is null.</param>
    /// <returns>A new instance of <see cref="Result{T}"/> with the specified value or an error.</returns>
    public static Result<T> Create<T>(T value, Error error)
        where T : class =>
        value is null ? Failure<T>(error) : Success(value);

    public static Result<int> Create(int value, Error error) =>
        value == default ? Failure<int>(error) : Success(value);

    /// <summary>
    /// Returns the first failure from the specified <paramref name="results"/>.
    /// If there is no failure, a success is returned.
    /// </summary>
    /// <param name="results">The results array.</param>
    /// <returns>
    /// The first failure from the specified <paramref name="results"/> array, or a success it does not exist.
    /// </returns>
    public static Result FirstFailureOrSuccess(params Result?[] results)
    {
        foreach (Result result in results.Where(result => result is not null && result.IsFailure))
        {
            return result;
        }

        return Success();
    }
}

public class Result<T> : Result
{
    private T _value;

    public T Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidOperationException(
                    $"You cannot access {nameof(Value)} when {nameof(IsFailure)} is true",
                    new Exception(Error!.Message!));
            }

            return _value;
        }
        set => _value = value;
    }

    /// <summary>
    /// Successful result.
    /// </summary>
    internal Result(T value) : base(true, Error.None)
    {
        Value = value;
    }

    /// <summary>
    /// Failure result.
    /// </summary>
    internal Result(Error error) : base(false, error)
    {
        Value = default!;
    }

    public static implicit operator Result<T>(T value) => new Result<T>(value);
}
