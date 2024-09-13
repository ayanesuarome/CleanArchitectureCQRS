using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Persistence.Migrations;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        CleanArchEFDbContext dbContext = scope.ServiceProvider.GetRequiredService<CleanArchEFDbContext>();
        dbContext.Database.Migrate();
    }
}
