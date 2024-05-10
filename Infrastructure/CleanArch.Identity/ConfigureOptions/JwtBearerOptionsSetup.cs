using CleanArch.Identity.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArch.Identity.ConfigureOptions;

internal sealed class JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = true,
            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key))
        };
    }
}
