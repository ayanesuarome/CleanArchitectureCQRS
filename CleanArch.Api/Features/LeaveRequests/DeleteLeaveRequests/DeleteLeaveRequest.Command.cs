using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;

public static partial class DeleteLeaveRequest
{
    public sealed record Command(int Id) : IRequest<Result>
    {
    }
}
