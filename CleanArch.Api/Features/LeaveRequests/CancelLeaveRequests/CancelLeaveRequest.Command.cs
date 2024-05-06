using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Entities;
using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public static partial class CancelLeaveRequest
{
    public sealed record Command(Guid Id) : ICommand<Result<LeaveRequest>>
    {
    }
}
