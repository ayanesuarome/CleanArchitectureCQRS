using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CleanArch.Persistence.Repositories;
using CleanArch.Persistence.Interceptors;
using CleanArch.Application.Abstractions.Data;
using CleanArch.Domain.LeaveTypes;
using CleanArch.Domain.LeaveAllocations;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddCleanArchEFDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddSingleton<SoftDeleteEntitiesInterceptor>();

        services.AddDbContext<CleanArchEFWriteDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString(CleanArchEFWriteDbContext.ConnectionStringName));
            options.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting });
            options.AddInterceptors(
                sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>(),
                sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                sp.GetRequiredService<SoftDeleteEntitiesInterceptor>());
        });

        services.AddDbContext<CleanArchEFReadDbContext>(
            options => options
                .UseSqlServer(configuration.GetConnectionString(CleanArchEFReadDbContext.ConnectionStringName))
                .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CleanArchEFWriteDbContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CleanArchEFReadDbContext>());

        return services;
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveRequestSummaryRepository, LeaveRequestSummaryRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

        return services;
    }
}
