using CleanArch.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArch.Benchmarking
{
    public abstract class EFDbContextInMemory : IDisposable
    {
        public readonly CleanArchEFDbContext context;
        private Mock<IPublisher> publisherMock;

        public EFDbContextInMemory()
        {
            DbContextOptions<CleanArchEFDbContext> dbOptions = new DbContextOptionsBuilder<CleanArchEFDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            publisherMock = new Mock<IPublisher>();

            context = new CleanArchEFDbContext(dbOptions, publisherMock.Object);
        }

        public void Dispose()
        {
            publisherMock = null;
            context.Dispose();
        }
    }
}
