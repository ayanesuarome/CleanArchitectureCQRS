using CleanArch.Application.Models;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypeDetails.Queries.GetLeaveTypesDetails;

public record GetLeaveTypeDetailsQuery(int Id) : IRequest<Result<LeaveTypeDetailsDto>>
{
}