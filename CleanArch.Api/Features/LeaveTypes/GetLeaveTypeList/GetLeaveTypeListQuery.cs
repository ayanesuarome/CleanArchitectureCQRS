using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public record GetLeaveTypeListQuery : IRequest<Result<List<LeaveTypeDto>>>;