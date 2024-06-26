﻿using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Domain.Authentication;
using CleanArch.Identity.Authentication;
using CleanArch.Identity.Options;
using CleanArch.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
            options.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting });
            options.UseSqlServer(configuration.GetConnectionString(CleanArchSqlServerDbContext));
        });

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<CleanArchIdentityEFDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureOptions<JwtSetup>();
        services.AddSingleton<IValidateOptions<JwtOptions>, JwtValidation>();
        services.ConfigureOptions<JwtOptionsSetup>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();

        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();
        services.AddScoped<IPermissionService, PermissionService>();

        return services;
    }
}
