using CleanArch.Application.Models.Errors;

namespace CleanArch.Application.Models
{
    public class ErrorResult : Result, IErrorResult
    {
        public string Message { get; }
        public IReadOnlyCollection<Error> Errors { get; }

        public ErrorResult(string message) : this(message, [])
        {
        }

        public ErrorResult(string message, IReadOnlyCollection<Error> errors)
        {
            Message = message;
            IsSuccess = false;
            Errors = errors;
        }
    }

    public class ErrorResult<T> : Result<T>, IErrorResult
    {
        public string Message { get; }
        public IReadOnlyCollection<Error> Errors { get; }

        public ErrorResult(string message) : this(message, [])
        {
        }

        public ErrorResult(string message, IReadOnlyCollection<Error> errors) : base(default)
        {
            Message = message;
            IsSuccess = false;
            Errors = errors;
        }
    }

    public class BadRequestResult : ErrorResult
    {
        public BadRequestResult(string message) : base(message)
        {
        }

        public BadRequestResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
        {
        }
    }

    public class BadRequestResult<T> : ErrorResult<T>
    {
        public BadRequestResult(string message) : base(message)
        {
        }

        public BadRequestResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
        {
        }
    }

    public class ValidationErrorResult : ErrorResult
    {
        public ValidationErrorResult(string message) : base(message)
        {
        }

        public ValidationErrorResult(string message, IReadOnlyCollection<ValidationError> errors) : base(message, errors)
        {
        }
    }

    public class ValidationError : Error
    {
        public ValidationError(string propertyName, string details) : base(null, details)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}
