﻿namespace CleanArch.Application.ResultPattern;

public sealed class NotFoundResult : Result
{
    public NotFoundResult(Error error) : base(false, error)
    {
    }
}

public sealed class NotFoundResult<T> : Result<T>
{
    public NotFoundResult(Error error) : base(error)
    {
    }
}