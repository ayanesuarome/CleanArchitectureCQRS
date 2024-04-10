using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public static partial class ChangeLeaveRequestApproval
{
    public sealed record Command(bool Approved) : IRequest<Result<LeaveRequest>>
    {
        public int Id { get; set; }
    }
}
