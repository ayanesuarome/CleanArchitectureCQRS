using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.Identity;
using CleanArch.BlazorUI.Providers;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace CleanArch.BlazorUI.Services;

internal sealed class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(IClient client,
        AuthenticationStateProvider authenticationStateProvider)
        : base(client)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(LoginVM model)
    {
        LoginRequest authRequest = new()
        {
            Email = model.Email,
            Password = model.Password
        };

        try
        {
            TokenResponse response = await _client.LoginAsync(authRequest);

            if (string.IsNullOrEmpty(response?.Token))
            {
                return false;
            }

            // set Claims in Blazor and login state
            await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedIn(response.Token);

            return true;
        }
        catch(ApiException e)
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
        RegistrationRequest request = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password
        };

        var response = await _client.RegisterAsync(request);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            return false;
        }

        return true;
    }
}
