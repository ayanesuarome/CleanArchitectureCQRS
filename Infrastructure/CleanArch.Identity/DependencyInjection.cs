using CleanArch.Application.Abstractions.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Identity.ConfigureOptions;
using CleanArch.Identity.Services;
using CleanArch.Identity.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArch.Identity;

public static class DependencyInjection
{
    private const string CleanArchIdentitySqlServerDbContext = "CleanArchIdentitySqlServerDbContext";

    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanArchIdentityEFDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(CleanArchIdentitySqlServerDbContext));
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<CleanArchIdentityEFDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

        services.ConfigureOptions<JwtSettingSetup>();

        services.AddSingleton<IValidateOptions<JwtSettings>, JwtSettingValidation>();
        services.ConfigureOptions<JwtBearerSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
            };);

        services.AddAuthorization();

        return services;
    }
}
