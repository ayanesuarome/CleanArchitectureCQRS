using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    internal sealed class Handler : IRequestHandler<Command, Result<LeaveRequest>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository _allocationRepository;
        private readonly IUserService _userService;
        private readonly IValidator<Command> _validator;

        public Handler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveAllocationRepository allocationRepository,
            IUserService userService,
            IValidator<Command> validator)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
            _allocationRepository = allocationRepository;
            _userService = userService;
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

            // check on employee's allocation
            LeaveAllocation leaveAllocation = await _allocationRepository.GetEmployeeAllocation(_userService.UserId, command.LeaveTypeId);

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

            LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(command);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
