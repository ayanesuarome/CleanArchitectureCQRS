using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveRequests;

public record LeaveRequestId(Guid Id) : IEntityKey
{
    public static implicit operator Guid(LeaveRequestId value) => value.Id;
}
