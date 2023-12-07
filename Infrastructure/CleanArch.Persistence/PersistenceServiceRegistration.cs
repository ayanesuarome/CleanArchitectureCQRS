using CleanArch.Persistence.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArch.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanArchEFDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CleanArchSqlServerDbContext"));
            options.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting });
        });

        return services;
    }
}
