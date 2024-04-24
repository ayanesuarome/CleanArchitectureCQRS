using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public static partial class ChangeLeaveRequestApproval
{
    public sealed record Command(Guid Id, bool Approved) : IRequest<Result<LeaveRequest>>
    {
    }
}
