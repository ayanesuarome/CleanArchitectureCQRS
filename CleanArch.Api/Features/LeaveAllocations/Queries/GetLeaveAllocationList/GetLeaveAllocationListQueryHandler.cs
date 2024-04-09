using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;

public class GetLeaveAllocationListQueryHandler(IMapper mapper, ILeaveAllocationRepository repository, IUserService userService)
    : IRequestHandler<GetLeaveAllocationListQuery, Result<List<LeaveAllocationDto>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveAllocationRepository _repository = repository;
    private readonly IUserService _userService = userService;

    public async Task<Result<List<LeaveAllocationDto>>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        string userId = _userService.UserId;
        List<LeaveAllocation> leaveAllocations = await _repository.GetLeaveAllocationsWithDetails(userId);
        List<LeaveAllocationDto> leaveAllocationDtos = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        return Result.Success<List<LeaveAllocationDto>>(leaveAllocationDtos);
    }
}
