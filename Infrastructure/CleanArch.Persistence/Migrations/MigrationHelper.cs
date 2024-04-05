using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Persistence.Migrations;

public static class MigrationHelper
{
    public static void ApplyMigration(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        CleanArchEFDbContext dbContext = scope.ServiceProvider.GetRequiredService<CleanArchEFDbContext>();

        if(dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
