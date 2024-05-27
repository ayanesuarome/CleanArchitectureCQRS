using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveTypes;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    public sealed record Query : IQuery<LeaveTypeListDto>;
}
