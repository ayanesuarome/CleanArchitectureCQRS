using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Data;
using CleanArch.Domain.Entities;
using CleanArch.Identity.ConfigureOptions;
using CleanArch.Identity.Services;
using CleanArch.Identity.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace CleanArch.Identity;

public static class DependencyInjection
{
    private const string CleanArchSqlServerDbContext = "CleanArchSqlServerDbContext";

    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
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
            options.TokenValidationParameters = jwtBearerOptions.TokenValidationParameters);

        services.AddAuthorization();

        services.AddDbContext<CleanArchIdentityEFDbContext>(options =>
        {
            options.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting });
            options.UseSqlServer(configuration.GetConnectionString(CleanArchSqlServerDbContext));
        });

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<CleanArchIdentityEFDbContext>()
            //.AddUserStore<UserStore<User, Role, CleanArchIdentityEFDbContext, int>>()
            //.AddRoleStore<RoleStore<Role, CleanArchIdentityEFDbContext, int>>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

        services.ConfigureOptions<JwtOptionsSetup>();
        services.AddSingleton<IValidateOptions<JwtOptions>, JwtSettingValidation>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        return services;
    }
}
