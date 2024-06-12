using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    public sealed record Query : IQuery<Response>;
}
