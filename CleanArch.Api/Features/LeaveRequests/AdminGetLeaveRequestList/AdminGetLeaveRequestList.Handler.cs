using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    public sealed class Handler(ILeaveRequestRepository repository, IUserService userService)
        : IQueryHandler<Query, Result<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _repository = repository;
        private readonly IUserService _userService = userService;

        public async Task<Result<LeaveRequestListDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync();
            List<LeaveRequestListDto.LeaveRequestDetailsModel> models = [];

            foreach(LeaveRequest leaveRequest in leaveRequests)
            {
                var employeeFullName = (await _userService.GetEmployee(leaveRequest.RequestingEmployeeId)).FullName;

                models.Add(new(
                    leaveRequest.Id,
                    leaveRequest.Range.StartDate.ToString(),
                    leaveRequest.Range.EndDate.ToString(),
                    leaveRequest.Comments?.Value,
                    leaveRequest.LeaveTypeId,
                    leaveRequest.LeaveTypeName.Value,
                    leaveRequest.RequestingEmployeeId,
                    employeeFullName,
                    leaveRequest.IsApproved,
                    leaveRequest.IsCancelled,
                    leaveRequest.DateCreated));
            }

            LeaveRequestListDto requestListDto = new(models);
            
            return new SuccessResult<LeaveRequestListDto>(requestListDto);
        }
    }
}
