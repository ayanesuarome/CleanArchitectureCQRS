using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;

public static partial class DeleteLeaveRequest
{
    public sealed record Command(Guid Id) : IRequest<Result>
    {
    }
}
