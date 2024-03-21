using CleanArch.Application.Models.Errors;

namespace CleanArch.Application.Models;

public abstract class Result
{
    public bool IsSuccess { get; protected set; }
    protected bool IsFailure => !IsSuccess;
    
    //public Error Error { get; }
    //public T? Data { get; }

    //private Result(bool isSuccess, Error error, T? data = default)
    //{
    //    if(isSuccess && error != Error.None ||
    //        !isSuccess && error == Error.None)
    //    {
    //        throw new ArgumentException("Invalid error", nameof(error));
    //    }

    //    IsSuccess = isSuccess;
    //    Error = error;
    //    Data = data;
    //}

    //public static Result<T> Success(T? data = default) => new(true, Error.None, data);
    //public static Result<T> Failure(Error error) => new(false, error);
}

public abstract class Result<T> : Result
{
    public T Data { get; }

    protected Result(T data)
    {
        if (!IsSuccess)
        {
            throw new Exception($"You cannot access {nameof(Data)} when {nameof(IsSuccess)} is false");
        }

        Data = data;
    }
}
