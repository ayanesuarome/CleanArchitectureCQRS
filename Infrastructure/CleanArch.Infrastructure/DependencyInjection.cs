using CleanArch.Application.Abstractions.Email;
using CleanArch.Application.Abstractions.Logging;
using CleanArch.Application.EventBus;
using CleanArch.Infrastructure.Emails;
using CleanArch.Infrastructure.Emails.Options;
using CleanArch.Infrastructure.Events;
using CleanArch.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<EmailSetup>();
        services.ConfigureOptions<EmailTemplateIdSetup>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        services.AddSingleton<InMemoryMessageQueue>();
        services.AddSingleton<IEventBus, EventBus>();
        services.AddHostedService<IntegrationEventProcessorJob>();

        return services;
    }
}
