using CleanArch.Domain.Entities;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public static partial class CancelLeaveRequest
{
    public sealed record Command(Guid Id) : ICommand<Result<LeaveRequest>>;
}
