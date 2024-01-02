using CleanArch.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Tests;

public class CleanArchEFDbContextFixture : IDisposable
{
    public readonly CleanArchEFDbContext context;

    public CleanArchEFDbContextFixture()
    {
        DbContextOptions<CleanArchEFDbContext> dbOptions = new DbContextOptionsBuilder<CleanArchEFDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        context = new CleanArchEFDbContext(dbOptions);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
