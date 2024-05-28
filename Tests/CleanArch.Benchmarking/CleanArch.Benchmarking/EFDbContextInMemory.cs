using CleanArch.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArch.Benchmarking
{
    public abstract class EFDbContextInMemory : IDisposable
    {
        public readonly CleanArchEFDbContext context;

        public EFDbContextInMemory()
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
}
