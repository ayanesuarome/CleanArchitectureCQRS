using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;

public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;