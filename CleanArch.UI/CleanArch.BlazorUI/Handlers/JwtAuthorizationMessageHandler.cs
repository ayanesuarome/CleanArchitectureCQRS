using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace CleanArch.BlazorUI.Handlers;

public class JwtAuthorizationMessageHandler(ILocalStorageService localStorage, IConfiguration configuration) : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly string _storageKey = configuration.GetValue<string>("StorageKey")
                                          ?? throw new ArgumentNullException("configuration['StorageKey']");

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        bool isTokenInLocalStorage = await _localStorage.ContainKeyAsync(_storageKey);

        if (isTokenInLocalStorage)
        {
            string token = await _localStorage.GetItemAsync<string>(_storageKey);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
