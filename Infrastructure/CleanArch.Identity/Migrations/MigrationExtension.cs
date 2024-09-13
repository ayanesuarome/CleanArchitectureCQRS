using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Identity.Migrations;

public static class MigrationExtension
{
    public static void ApplyIdentityMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        CleanArchIdentityEFDbContext dbContext = scope.ServiceProvider.GetRequiredService<CleanArchIdentityEFDbContext>();
        dbContext.Database.Migrate();
    }
}
