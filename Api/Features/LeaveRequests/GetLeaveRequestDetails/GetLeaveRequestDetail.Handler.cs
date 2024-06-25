using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    internal sealed class Handler : IQueryHandler<Query, LeaveRequestSummary>
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

        public async Task<Result<LeaveRequestSummary>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveRequestSummary leaveRequestSummary = await _repository.GetByIdAsync(new LeaveRequestId(query.Id));

            if (leaveRequestSummary is null)
            {
                return new NotFoundResult<LeaveRequestSummary>(DomainErrors.LeaveRequest.NotFound(query.Id));
            }

            return Result.Success<LeaveRequestSummary>(leaveRequestSummary);
        }
    }
}
