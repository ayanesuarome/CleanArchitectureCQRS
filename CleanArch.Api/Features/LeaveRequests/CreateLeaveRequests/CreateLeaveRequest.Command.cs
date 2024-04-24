using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed record Command(
        Guid LeaveTypeId,
        DateOnly StartDate,
        DateOnly EndDate,
        string? Comments) : IRequest<Result<LeaveRequest>>
    {
    }
}
