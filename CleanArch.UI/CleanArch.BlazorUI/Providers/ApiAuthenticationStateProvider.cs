using Blazored.LocalStorage;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArch.BlazorUI.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    // TODO: move to static class
    private const string StorageKey = "token";

    public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _jwtSecurityTokenHandler = new();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal user = new(new ClaimsIdentity());
        bool isTokenPresent = await _localStorage.ContainKeyAsync(StorageKey);
        
        if (!isTokenPresent)
        {
            return new AuthenticationState(user);
        }

        JwtSecurityToken tokenContent = await GetJwtSecurityToken();

        if(tokenContent.ValidTo < DateTimeOffset.Now)
        {
            await _localStorage.RemoveItemAsync(StorageKey);
            return new AuthenticationState(user);
        }

        List<Claim> claims = GetClaims(tokenContent);
        user = new(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }

    public async Task LoggedIn(string token)
    {
        await _localStorage.SetItemAsync(StorageKey, token);
        JwtSecurityToken tokenContent = await GetJwtSecurityToken();
        List<Claim> claims = GetClaims(tokenContent); 
        ClaimsPrincipal user = new(new ClaimsIdentity(claims, "jwt"));
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await _localStorage.RemoveItemAsync(StorageKey);
        ClaimsPrincipal nobody = new(new ClaimsIdentity());
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<JwtSecurityToken> GetJwtSecurityToken()
    {
        string savedToken = await _localStorage.GetItemAsync<string>(StorageKey);
        return _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
    }

    private List<Claim> GetClaims(JwtSecurityToken tokenContent)
    {
        List<Claim> claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));

        return claims;
    }
}
