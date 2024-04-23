using CleanArch.Application.Abstractions.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed class Handler : IRequestHandler<Command, Result<LeaveRequest>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository _allocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IValidator<Command> _validator;

        public Handler(
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveAllocationRepository allocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IUserIdentifierProvider userIdentifierProvider,
            IValidator<Command> validator)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _allocationRepository = allocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _userIdentifierProvider = userIdentifierProvider;
            _validator = validator;
        }

        public async Task<Result<LeaveRequest>> Handle(Command command, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<LeaveRequest>(
                    ValidationErrors.CreateLeaveRequest.CreateLeaveRequestValidation(validationResult.ToDictionary()));
            }

            Result<DateRange> rangeResult = DateRange.Create(command.StartDate, command.EndDate);
            Result<Comment>? commentResult = null;

            if(command.Comments is not null)
            {
                commentResult = Comment.Create(command.Comments);
            }

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(rangeResult, commentResult);
            
            if (firstFailureOrSuccess.IsFailure)
            {
                return new FailureResult<LeaveRequest>(firstFailureOrSuccess.Error);
            }

            LeaveType leaveType = await _leaveTypeRepository.GetByIdAsync(command.LeaveTypeId);

            if(leaveType is null)
            {
                return new FailureResult<LeaveRequest>(DomainErrors.LeaveRequest.LeaveTypeMustExist);
            }

            // check on employee's allocation
            LeaveAllocation leaveAllocation = await _allocationRepository.GetEmployeeAllocation(
                _userIdentifierProvider.UserId.ToString(),
                command.LeaveTypeId);

            // if allocations aren't enough, return validation error
            if (leaveAllocation is null)
            {
                return new FailureResult<LeaveRequest>(DomainErrors.LeaveRequest.NoAllocationsForLeaveType(command.LeaveTypeId));
            }

            Result hasEnoughDaysResult = leaveAllocation.ValidateHasEnoughDays(command.StartDate, command.EndDate);
            
            if (hasEnoughDaysResult.IsFailure)
            {
                return new FailureResult<LeaveRequest>(hasEnoughDaysResult.Error);
            }

            LeaveRequest leaveRequest = new(
                rangeResult.Value,
                leaveType,
                commentResult.Value,
                _userIdentifierProvider.UserId.ToString());

            await _leaveRequestRepository.CreateAsync(leaveRequest);

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
