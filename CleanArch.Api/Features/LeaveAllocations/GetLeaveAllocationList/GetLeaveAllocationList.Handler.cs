using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    internal sealed class Handler(ILeaveAllocationRepository repository, IUserIdentifierProvider userIdentifierProvider)
        : IQueryHandler<Query, Result<LeaveAllocationListDto>>
    {
        private readonly ILeaveAllocationRepository _repository = repository;
        private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;

        public async Task<Result<LeaveAllocationListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            Guid userId = _userIdentifierProvider.UserId;
            IReadOnlyCollection<LeaveAllocation> leaveAllocations = await _repository.GetLeaveAllocationsWithDetails(userId);

            LeaveAllocationListDto.LeaveAllocationModel[] dtos = leaveAllocations.Select(allocation =>
                new LeaveAllocationListDto.LeaveAllocationModel(
                    allocation.Id,
                    allocation.NumberOfDays,
                    allocation.Period,
                    allocation.LeaveTypeId)
                ).ToArray();

            LeaveAllocationListDto leaveAllocationDtos = new(dtos);

            return Result.Success(leaveAllocationDtos);
        }
    }
}
