using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository repository)
    : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveRequestRepository _repository = repository;

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetLeaveRequestWithDetailsAsync(request.Id);
        return _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);
    }
}
