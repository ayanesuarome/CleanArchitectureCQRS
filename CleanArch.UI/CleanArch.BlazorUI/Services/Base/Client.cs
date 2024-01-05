using System.Net;

namespace CleanArch.BlazorUI.Services.Base;

public partial class Client : IClient
{
    public HttpClient HttpClient => _httpClient;

    public Client()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.Credentials = new NetworkCredential();
        _httpClient = new HttpClient();
    }
}
