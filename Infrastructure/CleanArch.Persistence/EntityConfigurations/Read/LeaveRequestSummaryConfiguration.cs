using CleanArch.Domain.LeaveRequests;
using CleanArch.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Persistence.EntityConfigurations.Read;

internal sealed class LeaveRequestSummaryConfiguration : IEntityTypeConfiguration<LeaveRequestSummary>, IReadConfiguration
{
    public void Configure(EntityTypeBuilder<LeaveRequestSummary> builder)
    {
        builder.ToTable(TableNames.LeaveRequestSummaries);

        builder.HasKey(LeaveRequestSummary => LeaveRequestSummary.Id);

        builder
            .Property(leaveRequestSummary => leaveRequestSummary.Comments)
            .IsRequired(false);

        builder
            .Property(leaveRequestSummary => leaveRequestSummary.IsApproved)
            .IsRequired(false);
    }
}
