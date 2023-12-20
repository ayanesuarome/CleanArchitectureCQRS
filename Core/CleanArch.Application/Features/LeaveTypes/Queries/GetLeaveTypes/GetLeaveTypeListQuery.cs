using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;

public record GetLeaveTypeListQuery : IRequest<List<LeaveTypeDto>>;