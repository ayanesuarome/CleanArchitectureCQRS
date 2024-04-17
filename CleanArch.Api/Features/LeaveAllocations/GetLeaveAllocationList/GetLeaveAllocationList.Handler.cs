using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    internal sealed class Handler(IMapper mapper, ILeaveAllocationRepository repository, IUserService userService)
        : IRequestHandler<Query, Result<LeaveAllocationListDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveAllocationRepository _repository = repository;
        private readonly IUserService _userService = userService;

        public async Task<Result<LeaveAllocationListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            string userId = _userService.UserId;
            IReadOnlyCollection<LeaveAllocation> leaveAllocations = await _repository.GetLeaveAllocationsWithDetails(userId);

            IReadOnlyCollection<LeaveAllocationListDto.LeaveAllocationModel> list = _mapper
                .Map<IReadOnlyCollection<LeaveAllocationListDto.LeaveAllocationModel>>(leaveAllocations);

            LeaveAllocationListDto leaveAllocationDtos = new(list);

            return Result.Success(leaveAllocationDtos);
        }
    }
}
