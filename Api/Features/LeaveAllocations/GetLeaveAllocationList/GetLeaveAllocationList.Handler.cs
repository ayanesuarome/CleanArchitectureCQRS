﻿using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveAllocations;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    internal sealed class Handler : IQueryHandler<Query, Response>
    {
        private readonly ILeaveAllocationRepository _repository;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public Handler(
            ILeaveAllocationRepository repository,
            IUserIdentifierProvider userIdentifierProvider)
        {
            _repository = repository;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            Guid userId = _userIdentifierProvider.UserId;
            IReadOnlyCollection<LeaveAllocation> leaveAllocations = await _repository.GetLeaveAllocationsWithDetails(userId);

            Response.Model[] dtos = leaveAllocations.Select(allocation =>
                new Response.Model(
                    allocation.Id,
                    allocation.NumberOfDays,
                    allocation.Period,
                    allocation.LeaveTypeId)
                ).ToArray();

            Response leaveAllocationDtos = new(dtos);

            return Result.Success(leaveAllocationDtos);
        }
    }
}
