using CleanArch.Application.Models;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public record GetLeaveAllocationDetailsQuery(int Id) : IRequest<Result<LeaveAllocationDetailsDto>>
{
}
