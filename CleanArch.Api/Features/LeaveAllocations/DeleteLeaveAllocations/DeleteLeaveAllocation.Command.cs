using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;

public static partial class DeleteLeaveAllocation
{
    public sealed record Command(Guid Id) : IRequest<Result>
    {
    }
}
