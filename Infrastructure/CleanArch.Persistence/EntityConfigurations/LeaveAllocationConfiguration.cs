using CleanArch.Domain.Entities;
using CleanArch.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal sealed class LeaveAllocationConfiguration : IEntityTypeConfiguration<LeaveAllocation>
{
    public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
    {
        builder.ToTable(TableNames.LeaveAllocations);

        builder.HasKey(leaveAllocation =>  leaveAllocation.Id);

        builder.HasOne<LeaveType>()
            .WithMany()
            .HasForeignKey(leaveAllocation => leaveAllocation.LeaveTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        // IAuditableEntity
        builder.Property(leaveType => leaveType.DateCreated).IsRequired();
        builder.Property(leaveType => leaveType.CreatedBy).IsRequired();
        builder.Property(leaveType => leaveType.DateModified);
        builder.Property(leaveType => leaveType.ModifiedBy);
    }
}
