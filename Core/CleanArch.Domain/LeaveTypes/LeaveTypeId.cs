using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveTypes;

public record LeaveTypeId(Guid Id) : IEntityKey
{
    public static implicit operator Guid(LeaveTypeId value) => value.Id;
}
