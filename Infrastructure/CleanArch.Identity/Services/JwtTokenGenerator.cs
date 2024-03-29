﻿using CleanArch.Application.Models.Identity;
using CleanArch.Identity.Interfaces;
using CleanArch.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArch.Identity.Services;

public class JwtTokenGenerator(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings)
    : IJwtTokenGenerator
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<string> GenerateToken(ApplicationUser user)
    {
        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);
        IList<Claim> roleClaims = roles
            .Select(r => new Claim(ClaimTypes.Role, r))
            .ToList();

        IEnumerable<Claim> claims = new[]
        {
            // identifies the principal that is the subject of the JWT
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            // unique identifier for the JWT. It can be used to prevent the JWT from being replayed
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken securityToken = new(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
