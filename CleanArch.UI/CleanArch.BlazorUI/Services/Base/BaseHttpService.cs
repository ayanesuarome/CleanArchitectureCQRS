using System.Net;

namespace CleanArch.BlazorUI.Services.Base;

public abstract class BaseHttpService(IClient client)
{
    protected readonly IClient _client = client;

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException exception)
    {
        return exception.StatusCode switch
        {
            ((int)HttpStatusCode.BadRequest) =>
                new Response<Guid>()
                {
                    Message = "Invalid data was submitted.",
                    ValidationErrors = exception.Response,
                    Success = false
                },
            ((int)HttpStatusCode.NotFound) =>
                new Response<Guid>()
                {
                    Message = "The record was not found.",
                    Success = false
                },
            _ =>
                new Response<Guid>()
                {
                    Message = "Something went wrong, please try again later.",
                    Success = false
                }
        };
    }
}