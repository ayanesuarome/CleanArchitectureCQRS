using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;

public static partial class CreateLeaveAllocation
{
    public sealed record Command(Guid LeaveTypeId) : ICommand<Result<int>>
    {
    }
}
