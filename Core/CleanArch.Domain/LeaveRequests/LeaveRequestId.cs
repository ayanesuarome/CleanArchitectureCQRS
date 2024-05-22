namespace CleanArch.Domain.LeaveRequests;

public record LeaveRequestId(Guid Id)
{
    public static implicit operator Guid(LeaveRequestId value) => value.Id;
}
