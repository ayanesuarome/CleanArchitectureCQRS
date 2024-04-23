using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    public sealed record Command(
        int Id,
        string? Comments,
        DateOnly StartDate,
        DateOnly EndDate) : IRequest<Result<LeaveRequest>>
    {
    }
}
