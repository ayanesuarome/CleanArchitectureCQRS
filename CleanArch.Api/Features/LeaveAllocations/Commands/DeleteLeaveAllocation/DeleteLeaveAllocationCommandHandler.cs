﻿using CleanArch.Application.Features.LeaveAllocations.Shared;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository repository)
    : IRequestHandler<DeleteLeaveAllocationCommand, Result>
{
    private readonly ILeaveAllocationRepository _repository = repository;

    public async Task<Result> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
        {
            return new NotFoundResult(LeaveAllocationErrors.NotFound(request.Id));
        }

        await _repository.DeleteAsync(leaveAllocation);

        return new SuccessResult();
    }
}