using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;

public class GetLeaveTypeListQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    : IRequestHandler<GetLeaveTypeListQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<LeaveType> leaveTypes = await _repository.GetAsync();
        List<LeaveTypeDto> leaveTypeDtos = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        return leaveTypeDtos;
    }
}
