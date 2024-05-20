namespace CleanArch.Domain.ValueObjects;

public record LeaveRequestId(Guid Id)
{
    public static implicit operator Guid(LeaveRequestId value) => value.Id;
}
