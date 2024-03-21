using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository repository)
    : IRequestHandler<GetLeaveRequestDetailsQuery, Result<LeaveRequestDetailsDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveRequestRepository _repository = repository;

    public async Task<Result<LeaveRequestDetailsDto>> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetLeaveRequestWithDetailsAsync(request.Id);

        if (leaveRequest == null)
        {
            return new NotFoundResult<LeaveRequestDetailsDto>(LeaveRequestErrors.NotFound(request.Id));
        }

        LeaveRequestDetailsDto dto = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        return new SuccessResult<LeaveRequestDetailsDto>(dto);
    }
}
