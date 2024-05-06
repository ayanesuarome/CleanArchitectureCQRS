using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    public sealed record Query(Guid Id) : IQuery<Result<LeaveTypeDetailDto>>
    {
    }
}
