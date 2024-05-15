using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;

public static partial class DeleteLeaveAllocation
{
    public sealed record Command(Guid Id) : ICommand
    {
    }
}
