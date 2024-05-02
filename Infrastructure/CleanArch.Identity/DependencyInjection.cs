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

        services.ConfigureOptions<JwtOptionsSetup>();

        services.AddSingleton<IValidateOptions<JwtOptions>, JwtSettingValidation>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        JwtOptions jwtSettings = serviceProvider
            .GetRequiredService<IOptions<JwtOptions>>()
            .Value;
        JwtBearerOptions jwtBearerOptions = serviceProvider
            .GetRequiredService<IOptions<JwtBearerOptions>>()
            .Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => 
            options.TokenValidationParameters = jwtBearerOptions.TokenValidationParameters);

        services.AddAuthorization();

        return services;
    }
}
