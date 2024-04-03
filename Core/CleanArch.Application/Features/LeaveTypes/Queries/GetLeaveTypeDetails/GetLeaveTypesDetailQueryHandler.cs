using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Features.LeaveTypeDetails.Queries.GetLeaveTypesDetails;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypesdetail.Queries.GetLeaveTypesDetails;

public class GetLeaveTypesDetailQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        LeaveType leaveType = await _repository.GetByIdAsync(request.Id);

        if (leaveType is null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        return _mapper.Map<LeaveTypeDetailsDto>(leaveType);
    }
}
