namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    public sealed record Response
    {
        public Response(IReadOnlyCollection<Model> leaveTypes) => LeaveTypes = leaveTypes;

        public IReadOnlyCollection<Model> LeaveTypes { get; }

        public sealed record Model(Guid Id, string Name, int DefaultDays)
        {
        }
    }
}
