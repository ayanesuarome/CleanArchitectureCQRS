using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

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
