using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed record Command(
        Guid LeaveTypeId,
        DateOnly StartDate,
        DateOnly EndDate,
        string? Comments) : ICommand<Result<LeaveRequest>>
    {
    }
}
