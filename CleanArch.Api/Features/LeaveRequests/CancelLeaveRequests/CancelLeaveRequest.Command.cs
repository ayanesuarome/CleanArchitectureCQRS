using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public static partial class CancelLeaveRequest
{
    public sealed record Command(int Id) : IRequest<Result<LeaveRequest>>
    {
    }
}
