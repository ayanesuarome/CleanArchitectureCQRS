using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public static partial class ChangeLeaveRequestApproval
{
    public sealed record Command(Guid Id, bool Approved) : ICommand<LeaveRequest>
    {
    }
}
