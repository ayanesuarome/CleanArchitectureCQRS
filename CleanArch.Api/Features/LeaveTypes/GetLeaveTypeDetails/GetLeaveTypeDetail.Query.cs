using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    public sealed record Query(Guid Id) : IRequest<Result<LeaveTypeDetailDto>>
    {
    }
}
