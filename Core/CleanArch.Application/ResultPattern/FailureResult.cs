namespace CleanArch.Application.ResultPattern;

public class FailureResult : Result
{
    public string ErrorType => Error!.Type;
    public string ErrorMessage => Error!.Message;

    public FailureResult(Error error) : base(false, error)
    {
    }
}

public class FailureResult<T> : Result<T>
{
    public string ErrorType => Error!.Type;
    public string ErrorMessage => Error!.Message;

    public FailureResult(Error error) : base(error)
    {
    }

    public static implicit operator FailureResult(FailureResult<T> failureResult) => new FailureResult(failureResult.Error!);
}
