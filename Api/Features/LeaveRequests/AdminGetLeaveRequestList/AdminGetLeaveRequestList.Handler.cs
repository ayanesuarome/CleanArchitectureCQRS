using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
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
            IReadOnlyCollection<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync();
            List<Response.Model> models = [];

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

            Response requestListDto = new(models);
            
            return Result.Success<Response>(requestListDto);
        }
    }
}
