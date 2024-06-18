using CleanArch.Api.Contracts;
using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.Contracts;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    internal sealed class Handler : IQueryHandler<Query, Response>
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

        public async Task<Result<Response>> Handle(Query query, CancellationToken cancellationToken)
        {
            Guid userId = _userIdentifierProvider.UserId;

            PagedList<LeaveRequest> pagedList = await _repository.GetLeaveRequestsWithDetailsAsync(
                query.SearchTerm,
                query.SortColumn,
                query.SortOrder,
                query.Page,
                query.PageSize,
                userId);

            List<Response.Model> models = [];

            foreach (LeaveRequest leaveRequest in pagedList.Items)
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

            PagedList<Response.Model> pagedListDto = PagedList<Response.Model>.Map(pagedList, models);

            Response requestListDto = new(pagedListDto);

            return Result.Success<Response>(requestListDto);
        }
    }
}
