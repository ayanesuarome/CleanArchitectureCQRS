using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Persistence.Migrations;

public static class MigrationExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        CleanArchEFDbContext dbContext = scope.ServiceProvider.GetRequiredService<CleanArchEFDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
