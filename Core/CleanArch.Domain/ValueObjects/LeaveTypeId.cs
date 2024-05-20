namespace CleanArch.Domain.ValueObjects;

public record LeaveTypeId(Guid Id)
{
    public static implicit operator Guid(LeaveTypeId value) => value.Id;
}
