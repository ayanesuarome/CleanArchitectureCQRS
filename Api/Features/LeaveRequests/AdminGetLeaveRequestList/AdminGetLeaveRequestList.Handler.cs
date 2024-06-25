using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    internal sealed class Handler : IQueryHandler<Query, Response>
    {
        private readonly ILeaveRequestSummaryRepository _repository;
        private readonly IUserService _userService;

        public Handler(
            ILeaveRequestSummaryRepository repository,
            IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Result<Response>> Handle(Query query, CancellationToken cancellationToken)
        {
            PagedList<LeaveRequestSummary> pagedList = await _repository.GetLeaveRequestsWithDetailsAsync(
                query.SearchTerm,
                query.SortColumn,
                query.SortOrder,
                query.Page,
                query.PageSize);

            List<Response.Model> models = [];

            foreach(LeaveRequestSummary leaveRequest in pagedList.Items)
            {
                models.Add(new(
                    leaveRequest.Id,
                    leaveRequest.StartDate,
                    leaveRequest.EndDate,
                    leaveRequest.Comments,
                    leaveRequest.LeaveTypeId,
                    leaveRequest.LeaveTypeName,
                    leaveRequest.RequestingEmployeeId,
                    leaveRequest.EmployeeFullName,
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
