﻿using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    internal sealed class Handler : IQueryHandler<Query, LeaveRequestListDto>
    {
        private readonly ILeaveRequestRepository _repository;
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUserService _userService;

        public Handler(
            ILeaveRequestRepository repository,
            IUserIdentifierProvider userIdentifierProvider,
            IUserService userService)
        {
            _repository = repository;
            _userIdentifierProvider = userIdentifierProvider;
            _userService = userService;
        }

        public async Task<Result<LeaveRequestListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            Guid userId = _userIdentifierProvider.UserId;

            IReadOnlyCollection<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync(userId);
            List<LeaveRequestListDto.LeaveRequestDetailsModel> models = [];

            foreach (LeaveRequest leaveRequest in leaveRequests)
            {
                var employeeFullName = (await _userService.GetEmployee(userId)).FullName;
                models.Add(new(
                    leaveRequest.Id,
                    leaveRequest.Range.StartDate.ToString(),
                    leaveRequest.Range.EndDate.ToString(),
                    leaveRequest.Comments?.Value,
                    leaveRequest.LeaveTypeId,
                    leaveRequest.LeaveTypeName,
                    leaveRequest.RequestingEmployeeId,
                    employeeFullName,
                    leaveRequest.IsApproved,
                    leaveRequest.IsCancelled,
                    leaveRequest.DateCreated));
            }

            LeaveRequestListDto requestListDto = new(models);

            return Result.Success<LeaveRequestListDto>(requestListDto);
        }
    }
}