using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using CleanArch.Application.Behaviors;

namespace CleanArch.Api.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds and configures AutoMapper and MediatR services to the service collection.
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            });

            return services;
        }
    }
}
