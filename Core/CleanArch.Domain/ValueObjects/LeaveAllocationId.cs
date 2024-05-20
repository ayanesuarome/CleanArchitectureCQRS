namespace CleanArch.Domain.ValueObjects;

public record LeaveAllocationId(Guid Id)
{
    public static implicit operator Guid(LeaveAllocationId value) => value.Id;
}
