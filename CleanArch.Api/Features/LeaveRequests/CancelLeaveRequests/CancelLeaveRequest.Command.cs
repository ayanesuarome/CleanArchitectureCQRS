using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public static partial class CancelLeaveRequest
{
    public sealed record Command(Guid Id) : IRequest<Result<LeaveRequest>>
    {
    }
}
