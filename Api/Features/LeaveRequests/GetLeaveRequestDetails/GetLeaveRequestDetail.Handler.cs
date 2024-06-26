﻿using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    internal sealed class Handler : IQueryHandler<Query, Response>
    {
        private readonly ILeaveRequestRepository _repository;
        private readonly IUserService _userService;

        public Handler(
            ILeaveRequestRepository repository,
            IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Result<Response>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetLeaveRequestWithDetailsAsync(new LeaveRequestId(query.Id));

            if (leaveRequest is null)
            {
                return new NotFoundResult<Response>(DomainErrors.LeaveRequest.NotFound(query.Id));
            }

            Response dto = new(
                leaveRequest.Id,
                leaveRequest.Range.StartDate.ToString(),
                leaveRequest.Range.EndDate.ToString(),
                leaveRequest.Comments?.Value,
                leaveRequest.LeaveTypeId,
                leaveRequest.LeaveTypeName.Value,
                leaveRequest.RequestingEmployeeId,
                (await _userService.GetEmployee(leaveRequest.RequestingEmployeeId)).FullName,
                leaveRequest.IsApproved,
                leaveRequest.IsCancelled,
                leaveRequest.DateCreated);

            return Result.Success<Response>(dto);
        }
    }
}
