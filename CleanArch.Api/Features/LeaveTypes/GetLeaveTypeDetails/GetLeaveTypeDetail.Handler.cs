using AutoMapper;
using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    internal sealed class Handler(IMapper mapper, ILeaveTypeRepository repository)
        : IRequestHandler<Query, Result<LeaveTypeDetailDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<LeaveTypeDetailDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveType leaveType = await _repository.GetByIdAsync(query.Id);

            if (leaveType is null)
            {
                return new NotFoundResult<LeaveTypeDetailDto>(LeaveTypeErrors.NotFound(query.Id));
            }

            LeaveTypeDetailDto dto = _mapper.Map<LeaveTypeDetailDto>(leaveType);

            return new SuccessResult<LeaveTypeDetailDto>(dto);
        }
    }
}
