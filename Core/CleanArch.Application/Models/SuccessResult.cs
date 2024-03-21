namespace CleanArch.Application.Models;

public class SuccessResult : Result
{
    public SuccessResult()
    {
        IsSuccess = true;
    }
}

public class SuccessResult<T> : Result<T>
{
    public SuccessResult(T data) : base(data)
    {
        IsSuccess = true;
    }
}
