using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;

public record GetLeaveTypeListQuery : IRequest<Result<List<LeaveTypeDto>>>;