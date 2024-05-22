using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;
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

        builder.Property(leaveType => leaveType.Id)
            .HasConversion(
                leaveTypeId => leaveTypeId.Id,
                id => new LeaveTypeId(id));

        builder.HasIndex(leaveType => leaveType.Name)
            .IsUnique();

        builder.Property(leaveType => leaveType.Name)
            .HasConversion(
                name => name.Value,
                value => Name.Create(value).Value)
            .HasMaxLength(Name.MaxLength);

        //builder.ComplexProperty(leaveType => leaveType.Name, nameBuilder =>
        //{
        //    nameBuilder.Property(name => name.Value)
        //        .HasColumnName(nameof(LeaveType.Name))
        //        .HasMaxLength(Name.MaxLength)
        //        .IsRequired();
        //});

        builder.Property(leaveType => leaveType.DefaultDays)
            .HasMaxLength(DefaultDays.MaxValue)
            .IsRequired()
            .HasConversion(
                defaultDays => defaultDays.Value,
                value => DefaultDays.Create(value).Value);

        //builder.ComplexProperty(leaveType => leaveType.DefaultDays, defaultDaysBuilder =>
        //{
        //    defaultDaysBuilder.Property(defaultDays => defaultDays.Value)
        //        .HasColumnName(nameof(LeaveType.DefaultDays))
        //        .HasMaxLength(DefaultDays.MaxValue)
        //        .IsRequired();

        //});

        // IAuditableEntity
        builder.Property(leaveType => leaveType.DateCreated).IsRequired();
        builder.Property(leaveType => leaveType.CreatedBy).IsRequired();
        builder.Property(leaveType => leaveType.DateModified);
        builder.Property(leaveType => leaveType.ModifiedBy);
    }
}