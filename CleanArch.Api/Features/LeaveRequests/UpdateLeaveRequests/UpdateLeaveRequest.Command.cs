using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    public sealed record Command(int Id, string? RequestComments, bool IsCancelled)
        : BaseCommand, IRequest<Result<LeaveRequest>>
    {
    }
}
