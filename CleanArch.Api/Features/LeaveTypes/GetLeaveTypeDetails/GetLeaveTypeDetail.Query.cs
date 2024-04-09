using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    public sealed record Query(int Id) : IRequest<Result<LeaveTypeDetailDto>>
    {
    }
}
