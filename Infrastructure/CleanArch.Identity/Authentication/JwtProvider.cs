using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Domain.Entities;
using CleanArch.Identity.Constants;
using CleanArch.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArch.Identity.Services;

internal sealed class JwtProvider : IJwtProvider
{
    public JwtProvider(
        UserManager<User> userManager,
        IOptions<JwtOptions> jwtOptions,
        IPermissionService permissionService)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
        _permissionService = permissionService;
    }

    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtOptions;
    private readonly IPermissionService _permissionService;

    /// <inheritdoc />
    public async Task<string> GenerateTokenAsync(User user)
    {
        IEnumerable<string> roles = await _userManager.GetRolesAsync(user);
        IEnumerable<Claim> roleClaims = roles
            .Select(r => new Claim(ClaimTypes.Role, r))
            .ToList();

        HashSet<string> permissions = await _permissionService.GetPermissionsAsync(user.Id);
        IEnumerable<Claim> permissionClaims = permissions
            .Select(permission => new Claim(CustomClaims.Permissions, permission))
            .ToList();

        IEnumerable<Claim> claims = new[]
        {
            // identifies the principal that is the subject of the JWT
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            // unique identifier for the JWT. It can be used to prevent the JWT from being replayed
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.FullName),
            new Claim("uid", user.Id.ToString())
        }
        .Union(roleClaims)
        .Union(permissionClaims);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken securityToken = new(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims,
            null,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.DurationInMinutes),
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return tokenValue;
    }
}
