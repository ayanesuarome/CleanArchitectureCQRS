using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CleanArch.Persistence.Repositories;
using CleanArch.Persistence.Interceptors;
using CleanArch.Domain.Repositories;

namespace CleanArch.Persistence;

public static class PersistenceServiceRegistration
{
    private const string CleanArchSqlServerDbContext = "CleanArchSqlServerDbContext";

    public static IServiceCollection AddCleanArchEFDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanArchEFDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString(CleanArchSqlServerDbContext));
            options.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting });
            options.AddInterceptors(
                sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                sp.GetRequiredService<SoftDeleteEntitiesInterceptor>());
        });

        return services;
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddSingleton<SoftDeleteEntitiesInterceptor>();

        return services;
    }
}
