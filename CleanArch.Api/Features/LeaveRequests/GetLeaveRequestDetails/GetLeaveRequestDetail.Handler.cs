using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    internal sealed class Handler(ILeaveRequestRepository repository, IUserService userService)
        : IRequestHandler<Query, Result<LeaveRequestDetailsDto>>
    {
        private readonly ILeaveRequestRepository _repository = repository;
        private readonly IUserService _userService = userService;

        public async Task<Result<LeaveRequestDetailsDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetLeaveRequestWithDetailsAsync(query.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult<LeaveRequestDetailsDto>(DomainErrors.LeaveRequest.NotFound(query.Id));
            }

            LeaveRequestDetailsDto dto = new(
                    leaveRequest.Range.StartDate,
                    leaveRequest.Range.EndDate,
                    leaveRequest.RequestComments,
                    leaveRequest.LeaveTypeId,
                    leaveRequest.LeaveTypeName.Value,
                    leaveRequest.RequestingEmployeeId,
                    (await _userService.GetEmployee(leaveRequest.RequestingEmployeeId)).GetName(),
                    leaveRequest.IsApproved,
                    leaveRequest.IsCancelled,
                    leaveRequest.DateCreated);

            return new SuccessResult<LeaveRequestDetailsDto>(dto);
        }
    }
}
