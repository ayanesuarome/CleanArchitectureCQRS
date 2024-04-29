using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    public sealed class Handler(ILeaveRequestRepository repository, IUserService userService)
        : IRequestHandler<Query, Result<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _repository = repository;
        private readonly IUserService _userService = userService;

        public async Task<Result<LeaveRequestListDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync();
            Task<LeaveRequestDetailsDto>[] tasks = leaveRequests.Select(async leaveRequest =>
                new LeaveRequestDetailsDto(
                    leaveRequest.Id,
                    leaveRequest.Range.StartDate,
                    leaveRequest.Range.EndDate,
                    leaveRequest.Comments.Value,
                    leaveRequest.LeaveTypeId,
                    leaveRequest.LeaveTypeName.Value,
                    leaveRequest.RequestingEmployeeId,
                    (await _userService.GetEmployee(leaveRequest.RequestingEmployeeId)).FullName,
                    leaveRequest.IsApproved,
                    leaveRequest.IsCancelled,
                    leaveRequest.DateCreated)
                ).ToArray();

            LeaveRequestDetailsDto[] dtos = await Task.WhenAll(tasks);
            LeaveRequestListDto requestListDto = new(dtos);
            
            return new SuccessResult<LeaveRequestListDto>(requestListDto);
        }
    }
}
