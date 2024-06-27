using BenchmarkDotNet.Attributes;
using CleanArch.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Benchmarking;

public class OutBoxMessageBenchMarks : EFDbContextInMemory
{
    [Benchmark]
    public async Task<IReadOnlyCollection<OutboxMessage>> GetOutBoxMessagesWithEntityFramework()
    {
        return await Context
            .Set<OutboxMessage>()
            .Take(20)
            .OrderBy(message => message.OccurredOn)
            .ToListAsync();
    }

    [Benchmark]
    public async Task<IReadOnlyCollection<OutboxMessage>> GetOutBoxMessagesWithRawSql()
    {
        string getOutboxMessagesSql = @"
            SELECT Id, Content
            FROM OutboxMessages
            WHERE ProcessedOn IS NULL
            ORDER BY OccurredOn
            LIMIT 20";

        return await Context
            .Database
            .SqlQueryRaw<OutboxMessage>(getOutboxMessagesSql)
            .ToArrayAsync();
    }
}
