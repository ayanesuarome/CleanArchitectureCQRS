namespace CleanArch.Contracts.LeaveAllocations
{
    public sealed record UpdateLeaveAllocationRequest(int NumberOfDays, int Period)
    {
    }
}
