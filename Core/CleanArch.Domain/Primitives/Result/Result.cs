namespace CleanArch.Domain.Primitives.Result;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
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

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Success<T>(T data) => new(data);
    public static Result<T> Failure<T>(Error error) => new(error);
}

public class Result<T> : Result
{
    private T _data;

    public T Data
    {
        get
        {
            //if (IsFailure)
            //{
            //    throw new InvalidOperationException(
            //        $"You cannot access {nameof(Data)} when {nameof(IsFailure)} is true",
            //        new Exception(Error!.Message!));
            //}

            return _data;
        }
        set => _data = value;
    }

    /// <summary>
    /// Successful result.
    /// </summary>
    internal Result(T data) : base(true, Error.None)
    {
        Data = data;
    }

    /// <summary>
    /// Failure result.
    /// </summary>
    internal Result(Error error) : base(false, error)
    {
    }
}
