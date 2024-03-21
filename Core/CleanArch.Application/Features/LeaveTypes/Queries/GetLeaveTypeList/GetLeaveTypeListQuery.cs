using CleanArch.Application.Models;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;

public record GetLeaveTypeListQuery : IRequest<Result<List<LeaveTypeDto>>>;