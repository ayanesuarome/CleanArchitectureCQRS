using AutoMapper;
using Blazored.LocalStorage;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.Identity;
using CleanArch.BlazorUI.Providers;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace CleanArch.BlazorUI.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(IClient client,
        IMapper mapper,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage)
        : base(client, localStorage)
    {
        _mapper = mapper;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(LoginVM model)
    {
        AuthRequest authRequest = _mapper.Map<AuthRequest>(model);

        try
        {
            AuthResponse response = await _client.LoginAsync(authRequest);

            if (string.IsNullOrEmpty(response?.Token))
            {
                return false;
            }

            // set Claims in Blazor and login state
            await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedIn(response.Token);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task Logout()
    {
        // remove claims in Blazor and invalidate login state
        await((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
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
