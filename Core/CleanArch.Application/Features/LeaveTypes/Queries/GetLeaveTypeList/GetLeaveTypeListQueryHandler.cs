using AutoMapper;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;

public class GetLeaveTypeListQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    : IRequestHandler<GetLeaveTypeListQuery, Result<List<LeaveTypeDto>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;

    public async Task<Result<List<LeaveTypeDto>>> Handle(GetLeaveTypeListQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<LeaveType> leaveTypes = await _repository.GetAsync();
        List<LeaveTypeDto> leaveTypeDtos = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        return Result.Success<List<LeaveTypeDto>>(leaveTypeDtos);
    }
}
