using CleanArch.Domain.Entities;
using CleanArch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.HasKey(leaveRequest => leaveRequest.Id);

        builder
            // only one query filter per entity; use IgnoreQueryFilters() in combination with Where per entity
            .HasQueryFilter(property => !property.IsDeleted)
            // Filtered Index
            .HasIndex(property => property.IsDeleted)
                .HasFilter($"IsDeleted = 0");

        builder.HasOne<LeaveType>()
            .WithMany()
            .HasForeignKey(leaveRequest => leaveRequest.LeaveTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.ComplexProperty(leaveRequest => leaveRequest.LeaveTypeName, leaveTypeNameBuilder =>
        {
            leaveTypeNameBuilder.Property(leaveTypeName => leaveTypeName.Value)
                .HasColumnName(nameof(LeaveRequest.LeaveTypeName))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });
        
        builder.ComplexProperty(leaveRequest => leaveRequest.Comments, commentsBuilder =>
        {
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
        builder.Property(leaveRequest => leaveRequest.DateCreated)
            .IsRequired();
        builder.Property(leaveRequest => leaveRequest.CreatedBy)
            .IsRequired();
        builder.Property(leaveRequest => leaveRequest.DateModified);
        builder.Property(leaveRequest => leaveRequest.ModifiedBy);

        // IDeletableEntity
        builder.Property(leaveRequest => leaveRequest.DeletedOn);
        builder.Property(leaveRequest => leaveRequest.IsDeleted)
            .HasDefaultValue(false);
    }
}
