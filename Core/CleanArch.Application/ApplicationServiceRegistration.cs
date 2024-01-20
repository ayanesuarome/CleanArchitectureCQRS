using CleanArch.Application.Features.LeaveRequests.Shared;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArch.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }

    /// <summary>
    /// Adds and configures FluentValidation services to the service collection.
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<BaseLeaveRequestCommandValidator>(ServiceLifetime.Transient);

        return services;
    }
}
