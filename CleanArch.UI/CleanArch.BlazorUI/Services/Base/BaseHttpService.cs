using Blazored.LocalStorage;
using System.Net;
using System.Net.Http.Headers;

namespace CleanArch.BlazorUI.Services.Base;

public class BaseHttpService(IClient client, ILocalStorageService localStorage)
{
    protected readonly IClient _client = client;
    protected readonly ILocalStorageService _localStorage = localStorage;
    // TODO: move to static class
    protected const string StorageKey = "token";

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

    protected async Task AddBearerToken()
    {
        bool isTokenInLocalStorage = await _localStorage.ContainKeyAsync(StorageKey);

        if(isTokenInLocalStorage)
        {
            string token = await _localStorage.GetItemAsync<string>(StorageKey);
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}