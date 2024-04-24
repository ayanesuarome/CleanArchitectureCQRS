using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;

public static partial class CreateLeaveAllocation
{
    public sealed record Command(Guid LeaveTypeId) : IRequest<Result<int>>
    {
    }
}
