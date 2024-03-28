using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Extensions;
using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Result<LeaveRequest>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _allocationRepository;
    private readonly IUserService _userService;
    private readonly IValidator<CreateLeaveRequestCommand> _validator;

    public CreateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveAllocationRepository allocationRepository,
        IUserService userService,
        IValidator<CreateLeaveRequestCommand> validator)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _allocationRepository = allocationRepository;
        _userService = userService;
        _validator = validator;
    }

    public async Task<Result<LeaveRequest>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            return new FailureResult<LeaveRequest>(LeaveRequestErrors.InvalidLeaveRequest(validationResult.ToDictionary()));
        }

        // check on employee's allocation
        LeaveAllocation leaveAllocation = await _allocationRepository.GetEmployeeAllocation(_userService.UserId, request.LeaveTypeId);
        bool result = await _allocationRepository.HasEmployeeAllocation(_userService.UserId, request.LeaveTypeId);

        // if allocations aren't enough, return validation error
        if (leaveAllocation == null)
        {
            validationResult.AddError(
                nameof(request.LeaveTypeId),
                "You do not have any allocation for this leave type");

            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }

        if(!leaveAllocation.HasEnoughDays(request.StartDate, request.EndDate))
        {
            validationResult.AddError(nameof(request.EndDate), "You do not have enough days for this request");
            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }

        LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(request);
        await _leaveRequestRepository.CreateAsync(leaveRequest);

        return new SuccessResult<LeaveRequest>(leaveRequest);
    }
}
