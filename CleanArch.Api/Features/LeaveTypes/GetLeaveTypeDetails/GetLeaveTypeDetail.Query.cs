using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveTypes;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    public sealed record Query(Guid Id) : IQuery<LeaveTypeDetailDto>;
}
