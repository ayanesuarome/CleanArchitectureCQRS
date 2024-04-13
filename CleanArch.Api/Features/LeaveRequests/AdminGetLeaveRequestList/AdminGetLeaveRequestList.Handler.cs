using AutoMapper;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    internal sealed class Handler(IMapper mapper, ILeaveRequestRepository repository)
        : IRequestHandler<Query, Result<LeaveRequestListDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveRequestRepository _repository = repository;

        public async Task<Result<LeaveRequestListDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync();

            LeaveRequestListDto requestListDto = new(
                _mapper.Map<IReadOnlyCollection<LeaveRequestDetailsDto>>(leaveRequests));
            
            return new SuccessResult<LeaveRequestListDto>(requestListDto);
        }
    }
}
