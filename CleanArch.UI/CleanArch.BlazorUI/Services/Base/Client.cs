namespace CleanArch.BlazorUI.Services.Base;

public partial class Client : IClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClient HttpClient => _httpClient;

    public Client(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient();
        //_httpClient = new HttpClient();
    }
}
