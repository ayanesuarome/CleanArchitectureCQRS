using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    internal sealed class Handler(
        ILeaveRequestRepository repository,
        IUserIdentifierProvider userIdentifierProvider,
        IUserService userService)
    : IRequestHandler<Query, Result<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _repository = repository;
        private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
        private readonly IUserService _userService = userService;

        public async Task<Result<LeaveRequestListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            string userId = _userIdentifierProvider.UserId.ToString();

            IReadOnlyCollection<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync(userId);
            Task<LeaveRequestDetailsDto>[] tasks = leaveRequests.Select(async leaveRequest =>
                new LeaveRequestDetailsDto(
                    leaveRequest.Range.StartDate,
                    leaveRequest.Range.EndDate,
                    leaveRequest.Comments.Value,
                    leaveRequest.LeaveTypeId,
                    leaveRequest.LeaveTypeName.Value,
                    leaveRequest.RequestingEmployeeId,
                    (await _userService.GetEmployee(userId)).GetName(),
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
