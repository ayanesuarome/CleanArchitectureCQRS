using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _repository;

    public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        List<LeaveType> leaveTypes = await _repository.GetAsync();
        List<LeaveTypeDto> leaveTypeDtos = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        return leaveTypeDtos;
    }
}
