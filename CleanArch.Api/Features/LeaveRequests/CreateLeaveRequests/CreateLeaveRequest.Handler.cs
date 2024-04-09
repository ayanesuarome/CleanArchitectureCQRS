using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
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

        public async Task<Result<LeaveRequest>> Handle(Command request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<LeaveRequest>(LeaveRequestErrors.CreateLeaveRequestValidation(validationResult.ToString()));
            }

            // check on employee's allocation
            LeaveAllocation leaveAllocation = await _allocationRepository.GetEmployeeAllocation(_userService.UserId, request.LeaveTypeId);
            bool result = await _allocationRepository.HasEmployeeAllocation(_userService.UserId, request.LeaveTypeId);

            // if allocations aren't enough, return validation error
            if (leaveAllocation is null)
            {
                return new FailureResult<LeaveRequest>(LeaveRequestErrors.NotEnoughDays());
            }

            if (!leaveAllocation.HasEnoughDays(request.StartDate, request.EndDate))
            {
                return new FailureResult<LeaveRequest>(LeaveRequestErrors.NoAllocationsForLeaveType(request.LeaveTypeId));
            }

            LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
