using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed record Command(
        int LeaveTypeId,
        string? Comments,
        DateOnly StartDate,
        DateOnly EndDate) : IRequest<Result<LeaveRequest>>
    {
    }
}
