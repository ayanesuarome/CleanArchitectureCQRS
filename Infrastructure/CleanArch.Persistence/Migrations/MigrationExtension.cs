using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Persistence.Migrations;

public static class MigrationExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        CleanArchEFWriteDbContext writeDbContext = scope.ServiceProvider.GetRequiredService<CleanArchEFWriteDbContext>();
        writeDbContext.Database.Migrate();
        CleanArchEFReadDbContext readDbContext = scope.ServiceProvider.GetRequiredService<CleanArchEFReadDbContext>();
        readDbContext.Database.Migrate();
    }
}
