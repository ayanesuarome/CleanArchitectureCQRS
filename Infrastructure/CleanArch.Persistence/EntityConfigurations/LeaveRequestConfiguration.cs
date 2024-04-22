using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder
            // only one query filter per entity; use IgnoreQueryFilters() in combination with Where per entity
            .HasQueryFilter(property => !property.IsDeleted)
            // Filtered Index
            .HasIndex(property => property.IsDeleted)
                .HasFilter($"IsDeleted = 0");

        // IAuditableEntity
        builder.Property(property => property.DateCreated)
            .IsRequired();
        builder.Property(property => property.CreatedBy)
            .IsRequired();
        builder.Property(property => property.DateModified);
        builder.Property(property => property.ModifiedBy);

        // IDeletableEntity
        builder.Property(property => property.DeletedOn);
        builder.Property(property => property.IsDeleted)
            .HasDefaultValue(false);
    }
}
