﻿using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;

namespace CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;

public static partial class DeleteLeaveRequest
{
    internal sealed class Handler(ILeaveRequestRepository repository) : ICommandHandler<Command>
    {
        private readonly ILeaveRequestRepository _repository = repository;

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetByIdAsync(command.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult(DomainErrors.LeaveRequest.NotFound(command.Id));
            }

            _repository.Delete(leaveRequest);

            return new SuccessResult();
        }
    }
}
