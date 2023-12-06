using MediatR;

namespace CleanArch.Application.Features.LeaveTypesDetails.Queries.GetLeaveTypesDetails;

public record GetLeaveTypesDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;