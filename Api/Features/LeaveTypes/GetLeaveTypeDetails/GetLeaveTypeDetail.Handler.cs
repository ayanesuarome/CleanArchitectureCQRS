using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveTypes;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    internal sealed class Handler(ILeaveTypeRepository repository) : IQueryHandler<Query, Response>
    {
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<Response>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveType leaveType = await _repository.GetByIdAsync(new LeaveTypeId(query.Id));

            if (leaveType is null)
            {
                return new NotFoundResult<Response>(DomainErrors.LeaveType.NotFound(query.Id));
            }

            Response dto = new(
                leaveType.Id,
                leaveType.Name.Value,
                leaveType.DefaultDays.Value,
                leaveType.DateCreated);

            return Result.Success<Response>(dto);
        }
    }
}
