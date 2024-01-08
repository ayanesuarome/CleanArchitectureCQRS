using AutoMapper;
using Blazored.LocalStorage;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.I;
using CleanArch.BlazorUI.Models.Identity;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class AuthenticationService(IClient client, IMapper mapper, ILocalStorageService localStorage)
    : BaseHttpService(client, localStorage), IAuthenticationService
{
    private readonly IMapper _mapper = mapper;
    // TODO: move to static class
    private const string StorageKey = "token";

    public async Task<bool> Authenticate(string email, string password)
    {
        AuthRequest authRequest = new()
        {
            Email = email,
            Password = password
        };
        try
        {

            AuthResponse response = await _client.LoginAsync(authRequest);

            if (string.IsNullOrEmpty(response?.Token))
            {
                return false;
            }

            await _localStorage.SetItemAsync(StorageKey, response.Token);
            
            // TODO: Set Claims in Blazor and login state
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(StorageKey);
        // TODO: remove claims in Blazor and invalidate login state
    }

    public async Task<bool> RegisterAsync(RegistrationRequestVM model)
    {
        RegistrationRequest request = _mapper.Map<RegistrationRequest>(model);
        var response = await _client.RegisterAsync(request);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            return false;
        }

        return true;
    }
}
