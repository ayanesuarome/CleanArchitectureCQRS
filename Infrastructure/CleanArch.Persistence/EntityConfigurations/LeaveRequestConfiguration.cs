using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Domain.LeaveTypes;
using CleanArch.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal sealed class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.ToTable(TableNames.LeaveRequests);

        builder.HasKey(leaveRequest => leaveRequest.Id);

        builder.Property(leaveRequest => leaveRequest.Id)
            .HasConversion(
                leaveRequestId => leaveRequestId.Id,
                id => new LeaveRequestId(id));

        builder
            // only one query filter per entity; use IgnoreQueryFilters() in combination with Where per entity
            .HasQueryFilter(leaveRequest => !leaveRequest.IsDeleted)
            // Filtered Index
            .HasIndex(leaveRequest => leaveRequest.IsDeleted)
                .HasFilter($"{nameof(LeaveRequest.IsDeleted)} = 0");

        builder.HasOne<LeaveType>()
            .WithMany()
            .HasForeignKey(leaveRequest => leaveRequest.LeaveTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(leaveRequest => leaveRequest.LeaveTypeName)
            .HasConversion(
                leaveTypeName => leaveTypeName.Value,
                value => Name.Create(value).Value)
            .HasMaxLength(Name.MaxLength)
            .IsRequired();

        //builder.ComplexProperty(leaveRequest => leaveRequest.LeaveTypeName, leaveTypeNameBuilder =>
        //{
        //    leaveTypeNameBuilder.Property(leaveTypeName => leaveTypeName.Value)
        //        .HasColumnName(nameof(LeaveRequest.LeaveTypeName))
        //        .HasMaxLength(Name.MaxLength)
        //        .IsRequired();
        //});

        builder.OwnsOne(leaveRequest => leaveRequest.Comments, commentsBuilder =>
        {
            commentsBuilder.WithOwner();
            commentsBuilder.Property(comments => comments.Value)
                .HasColumnName(nameof(LeaveRequest.Comments))
                .HasMaxLength(Comment.MaxLength);
        });

        builder.ComplexProperty(leaveRequest => leaveRequest.Range, rangeBuilder =>
        {
            rangeBuilder.Property(range => range.StartDate)
                .HasColumnName(nameof(LeaveRequest.Range.StartDate))
                .IsRequired();

            rangeBuilder.Property(range => range.EndDate)
                .HasColumnName(nameof(LeaveRequest.Range.EndDate))
                .IsRequired();
        });

        builder.Ignore(leaveRequest => leaveRequest.DaysRequested);

        // IAuditableEntity
        builder
            .Property(leaveType => leaveType.DateCreated)
            .IsRequired();
        builder
            .Property(leaveType => leaveType.CreatedBy)
            .IsRequired();
        builder
            .Property(leaveType => leaveType.DateModified)
            .IsRequired(false);
        builder
            .Property(leaveType => leaveType.ModifiedBy)
            .IsRequired(false);

        // IDeletableEntity
        builder
            .Property(leaveRequest => leaveRequest.DeletedOn)
            .IsRequired(false);
        builder
            .Property(leaveRequest => leaveRequest.IsDeleted)
            .HasDefaultValue(false);
    }
}
