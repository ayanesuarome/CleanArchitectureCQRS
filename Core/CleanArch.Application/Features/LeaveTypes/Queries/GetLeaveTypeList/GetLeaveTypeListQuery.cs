using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;

public record GetLeaveTypeListQuery : IRequest<List<LeaveTypeDto>>;