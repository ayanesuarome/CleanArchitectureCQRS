using Blazored.LocalStorage;
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

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        
    }

    public async Task LoggedIn()
    {
        List<Claim> claims = await GetClaims();
        ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<List<Claim>> GetClaims()
    {
        string savedToken = await _localStorage.GetItemAsync<string>(StorageKey);
        JwtSecurityToken tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        List<Claim> claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));

        return claims;
    }
}
