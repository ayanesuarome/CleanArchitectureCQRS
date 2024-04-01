using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.Configurations;

internal class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        // only one query filter per entity; use IgnoreQueryFilters() in combination with Where per entity
        builder
            .HasQueryFilter(property => !property.IsDeleted)
            .HasIndex(property => property.IsDeleted)
                .HasFilter($"IsDeleted = 0");
    }
}
