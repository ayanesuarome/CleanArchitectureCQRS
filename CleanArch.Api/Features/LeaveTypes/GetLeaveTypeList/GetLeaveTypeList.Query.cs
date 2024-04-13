using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    public sealed record Query : IRequest<Result<LeaveTypeListDto>>;
}
