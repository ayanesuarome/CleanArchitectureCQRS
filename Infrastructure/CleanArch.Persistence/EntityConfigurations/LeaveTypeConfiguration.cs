using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        //builder.HasData(
        //    new LeaveType("Vacation", 10)
        //    {
        //        Id = 1,
        //    });

        builder.Property(property => property.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(property => property.DateCreated).IsRequired();
        builder.Property(property => property.CreatedBy).IsRequired();
        builder.Property(property => property.DateModified);
        builder.Property(property => property.ModifiedBy);
    }
}