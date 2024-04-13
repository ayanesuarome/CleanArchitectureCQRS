namespace CleanArch.Contracts.LeaveRequests;

public sealed record LeaveRequestListDto
{
    public LeaveRequestListDto(IReadOnlyCollection<LeaveRequestDetailsDto> leaveRequests) => LeaveRequests = leaveRequests;

    public IReadOnlyCollection<LeaveRequestDetailsDto> LeaveRequests { get; }
}
