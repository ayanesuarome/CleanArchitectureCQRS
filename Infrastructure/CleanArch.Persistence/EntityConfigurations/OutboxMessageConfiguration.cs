using CleanArch.Persistence.Constants;
using CleanArch.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableNames.OutboxMessages);

        builder.HasKey(message => message.Id);

        builder
            .Property(message => message.Id)
            .ValueGeneratedNever();

        builder
            .Property(message => message.Content)
            .IsRequired();

        builder
            .Property(message => message.Type)
            .IsRequired();

        builder
            .Property(message => message.OccurredOn)
            .IsRequired();

        builder
            .Property(message => message.Error)
            .IsRequired(false);

        builder
            .Property(message => message.ProcessedOn)
            .IsRequired(false);

        builder
            // only one query filter per entity; use IgnoreQueryFilters() in combination with Where per entity
            .HasQueryFilter(message => message.ProcessedOn == null)
            .HasIndex(message => message.ProcessedOn)
                .HasFilter($"{nameof(OutboxMessage.ProcessedOn)} IS NULL");
    }
}