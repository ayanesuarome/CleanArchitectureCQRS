using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace CleanArch.BlazorUI.Handlers;

public class JwtAuthorizationMessageHandler(ILocalStorageService localStorage) : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage = localStorage;
    // TODO: move to static class
    protected const string StorageKey = "token";

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        bool isTokenInLocalStorage = await _localStorage.ContainKeyAsync(StorageKey);

        if (isTokenInLocalStorage)
        {
            string token = await _localStorage.GetItemAsync<string>(StorageKey);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
