namespace CleanArch.Domain.Core.Primitives.Result;

public interface INotFoundResult { }

public sealed class NotFoundResult : Result, INotFoundResult
{
    public NotFoundResult(Error error) : base(false, error)
    {
    }
}

public sealed class NotFoundResult<T> : Result<T>, INotFoundResult
{
    public NotFoundResult(Error error) : base(error)
    {
    }
}
