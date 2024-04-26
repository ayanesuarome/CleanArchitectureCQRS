using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Identity.Migrations;

public static class MigrationExtension
{
    public static void ApplyIdentityMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        CleanArchIdentityEFDbContext dbContext = scope.ServiceProvider.GetRequiredService<CleanArchIdentityEFDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
