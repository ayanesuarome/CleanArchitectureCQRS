using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArch.BlazorUI.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly string _storageKey;

    public ApiAuthenticationStateProvider(ILocalStorageService localStorage, IConfiguration configuration)
    {
        _localStorage = localStorage;
        _storageKey = configuration.GetValue<string>("StorageKey")!;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal user = new(new ClaimsIdentity());
        bool isTokenPresent = await _localStorage.ContainKeyAsync(_storageKey).ConfigureAwait(false);
        
        if (!isTokenPresent)
        {
            return new AuthenticationState(user);
        }

        JwtSecurityToken tokenContent = await GetJwtSecurityToken().ConfigureAwait(false);

        if(tokenContent.ValidTo < DateTimeOffset.Now)
        {
            await _localStorage.RemoveItemAsync(_storageKey).ConfigureAwait(false);
            return new AuthenticationState(user);
        }

        List<Claim> claims = GetClaims(tokenContent);
        user = new(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }

    public async Task LoggedIn(string token)
    {
        await _localStorage.SetItemAsync(_storageKey, token).ConfigureAwait(false);
        JwtSecurityToken tokenContent = await GetJwtSecurityToken().ConfigureAwait(false);
        List<Claim> claims = GetClaims(tokenContent); 
        ClaimsPrincipal user = new(new ClaimsIdentity(claims, "jwt"));
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await _localStorage.RemoveItemAsync(_storageKey).ConfigureAwait(false);
        ClaimsPrincipal nobody = new(new ClaimsIdentity());
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<JwtSecurityToken> GetJwtSecurityToken()
    {
        string? savedToken = await _localStorage.GetItemAsync<string>(_storageKey).ConfigureAwait(false);
        return _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
    }

    private static List<Claim> GetClaims(JwtSecurityToken tokenContent)
    {
        List<Claim> claims = tokenContent.Claims.ToList();
        // TODO: get name
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}
