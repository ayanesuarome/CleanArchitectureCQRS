namespace CleanArch.Application.ResultPattern;

public sealed class SuccessResult : Result
{
    public SuccessResult() : base(true, Error.None)
    {
    }
}

public sealed class SuccessResult<T> : Result<T>
{
    public SuccessResult(T data) : base(data)
    {
    }

    public static implicit operator SuccessResult(SuccessResult<T> _) => new SuccessResult();
}
