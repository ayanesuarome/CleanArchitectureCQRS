using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public record GetLeaveTypeDetailsQuery(int Id) : IRequest<Result<LeaveTypeDetailsDto>>
{
}