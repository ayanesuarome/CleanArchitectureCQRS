using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Features.LeaveTypesDetails.Queries.GetLeaveTypesDetails;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypesdetails.Queries.GetLeaveTypesDetails;

public class GetLeaveTypesDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    : IRequestHandler<GetLeaveTypesDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypesDetailsQuery request, CancellationToken cancellationToken)
    {
        LeaveType leaveType = await _repository.GetByIdAsync(request.Id);

        if (leaveType == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        LeaveTypeDetailsDto leaveTypeDetails = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

        return leaveTypeDetails;
    }
}
