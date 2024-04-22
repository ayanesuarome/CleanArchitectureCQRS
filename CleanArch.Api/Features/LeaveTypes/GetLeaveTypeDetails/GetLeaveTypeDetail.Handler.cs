using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    internal sealed class Handler(ILeaveTypeRepository repository)
        : IRequestHandler<Query, Result<LeaveTypeDetailDto>>
    {
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<LeaveTypeDetailDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveType leaveType = await _repository.GetByIdAsync(query.Id);

            if (leaveType is null)
            {
                return new NotFoundResult<LeaveTypeDetailDto>(DomainErrors.LeaveType.NotFound(query.Id));
            }

            LeaveTypeDetailDto dto = new(
                leaveType.Id,
                leaveType.Name.Value,
                leaveType.DefaultDays.Value,
                leaveType.DateCreated);

            return new SuccessResult<LeaveTypeDetailDto>(dto);
        }
    }
}
