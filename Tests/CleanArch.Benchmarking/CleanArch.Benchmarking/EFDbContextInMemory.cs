using CleanArch.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Benchmarking
{
    public abstract class EFDbContextInMemory : IDisposable
    {
        internal readonly CleanArchEFWriteDbContext Context;

        public EFDbContextInMemory()
        {
            DbContextOptions<CleanArchEFWriteDbContext> dbOptions = new DbContextOptionsBuilder<CleanArchEFWriteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new CleanArchEFWriteDbContext(dbOptions);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
