using CleanArch.Application.Abstractions.Email;
using CleanArch.Application.Abstractions.Logging;
using CleanArch.Infrastructure.ConfigureOptions;
using CleanArch.Infrastructure.Logging;
using CleanArch.Infrastructure.Services.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infrastructure;

public static class InfrastructureServiceRegistration 
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<EmailSettingSetup>();
        services.ConfigureOptions<EmailTemplateIdSetup>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}
