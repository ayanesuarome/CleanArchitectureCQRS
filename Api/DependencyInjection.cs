using System.Reflection;
using CleanArch.Api.Behaviors;
using FluentValidation;

namespace CleanArch.Api
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds and configures AutoMapper and MediatR services to the service collection.
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(),
                includeInternalTypes: true);
            //services.AddScoped<IValidator<BaseCommand>, BaseCommandValidator>();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddHealthChecks();

            return services;
        }
    }
}
