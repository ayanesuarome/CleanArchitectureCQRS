namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    public sealed record Response(Guid Id, string Name, int DefaultDays, DateTimeOffset? DateCreated);
}
