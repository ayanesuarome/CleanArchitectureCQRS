using CleanArch.Persistence;
using CleanArch.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Benchmarking
{
    public abstract class EFDbContextInMemory : IDisposable
    {
        internal readonly CleanArchEFWriteDbContext Context;

        protected EFDbContextInMemory()
        {
            DbContextOptions<CleanArchEFWriteDbContext> dbOptions = new DbContextOptionsBuilder<CleanArchEFWriteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new CleanArchEFWriteDbContext(dbOptions);

            var messages = new List<OutboxMessage>
            {
                new(Guid.NewGuid(), DateTimeOffset.Now, "order", "content"),
                new(Guid.NewGuid(), DateTimeOffset.Now, "product", "content")
            };

            Context.Set<OutboxMessage>().AddRange(messages);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
