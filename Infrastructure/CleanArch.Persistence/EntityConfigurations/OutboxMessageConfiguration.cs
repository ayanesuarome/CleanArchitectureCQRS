using CleanArch.Persistence.Constants;
using CleanArch.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableNames.OutBoxMessages);

        builder.HasKey(message => message.Id);

        builder
            // only one query filter per entity; use IgnoreQueryFilters() in combination with Where per entity
            .HasQueryFilter(message => message.ProcessedOn != null)
            .HasIndex(message => message.ProcessedOn)
                .HasFilter($"{nameof(OutboxMessage.ProcessedOn)} IS NULL");
    }
}