using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Identity.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArch.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    // TODO: move to a static class
    private const string EmployeeRole = "Employee";

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

        if(user == null)
        {
            throw new BadRequestException("Invalid email or password");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if(!result.Succeeded)
        {
            throw new BadRequestException("Invalid email or password");
        }

        JwtSecurityToken securityToken = await GenerateToken(user);

        return new AuthResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
        };
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        RegistrationRequestValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid registration", validationResult);
        }

        ApplicationUser user = new()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if(!result.Succeeded)
        {
            StringBuilder errors = new();

            foreach (IdentityError error in result.Errors)
            {
                errors.AppendFormat("-{0}\n", error.Description);
            }
            throw new BadRequestException(result.Errors.ToString());
        }

        await _userManager.AddToRoleAsync(user, EmployeeRole);

        return new RegistrationResponse(user.Id);
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);
        IList<Claim> roleClaims = roles
            .Select(r => new Claim(ClaimTypes.Role, r))
            .ToList();

        IEnumerable<Claim> claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // should be unique for every token
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
    }
}
