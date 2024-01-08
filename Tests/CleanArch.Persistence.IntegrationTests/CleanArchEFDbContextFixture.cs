using CleanArch.Application.Interfaces.Identity;
using CleanArch.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArch.Persistence.Tests;

public class CleanArchEFDbContextFixture : IDisposable
{
    public readonly CleanArchEFDbContext context;
    public Mock<IUserService> userServiceMock;

    public CleanArchEFDbContextFixture()
    {
        DbContextOptions<CleanArchEFDbContext> dbOptions = new DbContextOptionsBuilder<CleanArchEFDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        userServiceMock = new Mock<IUserService>();

        context = new CleanArchEFDbContext(dbOptions, userServiceMock.Object);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
