using CleanArch.Application.Abstractions.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CleanArch.Integration.Tests.TestConfigurations;

internal static class ServiceCollectionExtensions
{
    public static void AddUserIdentifierProviderMock(this IServiceCollection services)
    {
        ServiceDescriptor? descriptor = services
            .SingleOrDefault(service => service.ServiceType == typeof(IUserIdentifierProvider));

        if (descriptor is not null)
        {
            services.Remove(descriptor);

            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                Mock<IUserIdentifierProvider> mock = new();
                mock.Setup(a => a.UserId).Returns(Guid.Parse("f8b3c041-3397-43f1-95db-6fd3b5eb2e40"));
                return mock.Object;
            }));
        }
    }

    public static void EnsureDbCreated<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<TDbContext>();
        context.Database.EnsureCreated();
    }
}
