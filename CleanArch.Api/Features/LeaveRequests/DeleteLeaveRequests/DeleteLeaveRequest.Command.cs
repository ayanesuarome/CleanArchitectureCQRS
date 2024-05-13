using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;

public static partial class DeleteLeaveRequest
{
    public sealed record Command(Guid Id) : ICommand<Result>
    {
    }
}
