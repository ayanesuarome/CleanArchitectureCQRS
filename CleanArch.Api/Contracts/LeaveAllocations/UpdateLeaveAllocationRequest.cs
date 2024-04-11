namespace CleanArch.Api.Contracts.LeaveAllocations
{
    public sealed record UpdateLeaveAllocationRequest(int LeaveTypeId, int NumberOfDays, int Period)
    {
    }
}
