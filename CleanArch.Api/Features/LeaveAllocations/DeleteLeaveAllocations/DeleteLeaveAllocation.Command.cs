using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;

public static partial class DeleteLeaveAllocation
{
    public sealed record Command(Guid Id) : ICommand<Result>;
}
