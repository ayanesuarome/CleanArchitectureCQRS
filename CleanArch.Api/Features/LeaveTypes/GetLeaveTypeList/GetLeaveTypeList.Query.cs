using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    public sealed record Query : IRequest<Result<LeaveTypeListDto>>;
}
