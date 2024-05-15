using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;

public static partial class CreateLeaveAllocation
{
    public sealed record Command(Guid LeaveTypeId) : ICommand<int>
    {
    }
}
