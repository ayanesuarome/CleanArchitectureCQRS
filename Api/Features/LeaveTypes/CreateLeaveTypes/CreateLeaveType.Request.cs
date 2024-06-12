namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    public sealed record Request(string Name, int DefaultDays);
}
