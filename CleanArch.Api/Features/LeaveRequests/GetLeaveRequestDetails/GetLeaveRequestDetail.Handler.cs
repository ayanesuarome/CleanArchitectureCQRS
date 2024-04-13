using AutoMapper;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    internal sealed class Handler(IMapper mapper, ILeaveRequestRepository repository)
        : IRequestHandler<Query, Result<LeaveRequestDetailsDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveRequestRepository _repository = repository;

        public async Task<Result<LeaveRequestDetailsDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetLeaveRequestWithDetailsAsync(query.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult<LeaveRequestDetailsDto>(LeaveRequestErrors.NotFound(query.Id));
            }

            LeaveRequestDetailsDto dto = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

            return new SuccessResult<LeaveRequestDetailsDto>(dto);
        }
    }
}
