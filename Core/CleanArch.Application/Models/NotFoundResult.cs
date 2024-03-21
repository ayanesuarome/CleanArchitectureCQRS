using CleanArch.Application.Models.Errors;

namespace CleanArch.Application.Models;

public class NotFoundResult : Result
{
    public Error Error { get; }

    public NotFoundResult(Error error)
    {
        Error = error;
        IsSuccess = false;
    }
}

public class NotFoundResult<T> : Result<T>
{
    public Error Error { get; }

    public NotFoundResult(Error error) : base(default)
    {
        Error = error;
        IsSuccess = false;
    }
}
