using System.Net;

namespace CleanArch.BlazorUI.Services.Base;

public partial class Client : IClient
{
    public HttpClient HttpClient => _httpClient;

    public Client()
    {
        _httpClient = new HttpClient();
    }
}
