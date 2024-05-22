namespace CleanArch.Domain.LeaveTypes;

public record LeaveTypeId(Guid Id)
{
    public static implicit operator Guid(LeaveTypeId value) => value.Id;
}
