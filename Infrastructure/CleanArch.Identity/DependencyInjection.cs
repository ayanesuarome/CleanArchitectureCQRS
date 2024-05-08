using CleanArch.Application.Abstractions.Authentication;
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
    private const string CleanArchSqlServerDbContext = "CleanArchSqlServerDbContext";

    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanArchIdentityEFDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(CleanArchSqlServerDbContext));
        });

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        JwtBearerOptions jwtBearerOptions = serviceProvider
            .GetRequiredService<IOptions<JwtBearerOptions>>()
            .Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
            options.TokenValidationParameters = jwtBearerOptions.TokenValidationParameters)
        .AddCookie(IdentityConstants.ApplicationScheme);

        services.AddAuthorization();

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<CleanArchIdentityEFDbContext>()
            // configure the required services for the Identity endpoints added in .Net8
            .AddApiEndpoints();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

        services.ConfigureOptions<JwtOptionsSetup>();
        services.AddSingleton<IValidateOptions<JwtOptions>, JwtSettingValidation>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        return services;
    }
}
