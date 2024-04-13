using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    public sealed record Query(int Id) : IRequest<Result<LeaveTypeDetailDto>>
    {
    }
}
