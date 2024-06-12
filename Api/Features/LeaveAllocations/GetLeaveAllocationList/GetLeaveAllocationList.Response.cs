namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    public sealed record Response
    {
        public Response(IReadOnlyCollection<Model> leaveAllocationList) => LeaveAllocationList = leaveAllocationList;
        public IReadOnlyCollection<Model> LeaveAllocationList { get; }

        public sealed record Model(Guid Id, int NumberOfDays, int Period, Guid LeaveTypeId)
        {
        }
    }
}
