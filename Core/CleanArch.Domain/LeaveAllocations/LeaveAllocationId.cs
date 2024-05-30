using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveAllocations;

public record LeaveAllocationId(Guid Id) : IEntityKey
{
    public static implicit operator Guid(LeaveAllocationId value) => value.Id;
}
