using CleanArch.Domain.Entities;
using CleanArch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
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

        builder.Property(leaveType => leaveType.Name)
            .IsRequired()
            .HasMaxLength(100);

        // IAuditableEntity
        builder.Property(leaveType => leaveType.DateCreated).IsRequired();
        builder.Property(leaveType => leaveType.CreatedBy).IsRequired();
        builder.Property(leaveType => leaveType.DateModified);
        builder.Property(leaveType => leaveType.ModifiedBy);
    }
}