using CleanArch.Api.Features.LeaveRequests;
using FluentValidation;

namespace CleanArch.Api.Infrastructure
{
    public static class ApiServiceRegistration
    {
        /// <summary>
        /// Adds and configures FluentValidation services to the service collection.
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<BaseCommandValidator>(ServiceLifetime.Transient);

            return services;
        }
    }
}
