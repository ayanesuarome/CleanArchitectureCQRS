using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    public sealed record Query : IQuery<Result<LeaveTypeListDto>>;
}
