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

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        JwtSettings jwtSettings = serviceProvider
            .GetRequiredService<IOptions<JwtSettings>>()
            .Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });

        services.AddAuthorization();

        return services;
    }
}
