using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Identity.DatabaseContext;
using CleanArch.Identity.Models;
using CleanArch.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArch.Identity;

public static class IdentityServiceRegistration
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

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();

        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
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
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });

        return services;
    }
}
