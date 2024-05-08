using CleanArch.Domain.Entities;
using CleanArch.Domain.ValueObjects;
using CleanArch.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal sealed class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.ToTable(TableNames.LeaveTypes);

        builder.HasKey(leaveType => leaveType.Id);

        builder.ComplexProperty(leaveType => leaveType.Name, nameBuilder =>
        {
            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(LeaveType.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.ComplexProperty(leaveType => leaveType.DefaultDays, defaultDaysBuilder =>
        {
            defaultDaysBuilder.Property(defaultDays => defaultDays.Value)
                .HasColumnName(nameof(LeaveType.DefaultDays))
                .HasMaxLength(DefaultDays.MaxValue)
                .IsRequired();

        });

        // IAuditableEntity
        builder.Property(leaveType => leaveType.DateCreated).IsRequired();
        builder.Property(leaveType => leaveType.CreatedBy).IsRequired();
        builder.Property(leaveType => leaveType.DateModified);
        builder.Property(leaveType => leaveType.ModifiedBy);
    }
}