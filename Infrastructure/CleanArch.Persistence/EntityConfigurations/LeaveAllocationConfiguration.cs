using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations;

internal class LeaveAllocationConfiguration : IEntityTypeConfiguration<LeaveAllocation>
{
    public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
    {
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
