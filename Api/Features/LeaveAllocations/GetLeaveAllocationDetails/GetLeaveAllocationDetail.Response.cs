namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    public sealed record Response(int NumberOfDays, int Period, Guid LeaveTypeId);
}