using CleanArch.Persistence.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CleanArch.Persistence.Repositories;
using CleanArch.Domain.Repositories;
using CleanArch.Persistence.Interceptors;

namespace CleanArch.Persistence;

public static class PersistenceServiceRegistration
{
    private const string CleanArchSqlServerDbContext = "CleanArchSqlServerDbContext";

    public static IServiceCollection AddCleanArchEFDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanArchEFDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(CleanArchSqlServerDbContext));
            options.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting });
            options.AddInterceptors();
        });

        return services;
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddSingleton<SoftDeleteEntitiesInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        return services;
    }
}
