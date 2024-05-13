using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Entities;
using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    public sealed record Command(
        Guid Id,
        DateOnly StartDate,
        DateOnly EndDate,
        string? Comments) : ICommand<Result<LeaveRequest>>
    {
    }
}
