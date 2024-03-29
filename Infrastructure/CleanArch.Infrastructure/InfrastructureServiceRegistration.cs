﻿using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models.Emails;
using CleanArch.Infrastructure.EmailService;
using CleanArch.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infrastructure;

public static class InfrastructureServiceRegistration 
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
        services.Configure<EmailTemplateIds>(configuration.GetSection(nameof(EmailTemplateIds)));
        services.AddTransient<IEmailSender, EmailSender>();
        
        // it depends on the way of using ExceptionMiddleware registered as scoped or the new feature in .NET8 of IExceptionHandler as singleton
        //services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}
