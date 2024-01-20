using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _allocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IValidator<CreateLeaveAllocationCommand> _validator;

    public CreateLeaveAllocationCommandHandler(IMapper mapper,
        ILeaveAllocationRepository allocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IValidator<CreateLeaveAllocationCommand> validator)
    {
        _mapper = mapper;
        _allocationRepository = allocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _validator = validator;
    }

    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveAllocation)}", validationResult);
        }

        LeaveAllocation leaveAllocation = _mapper.Map<LeaveAllocation>(request);
        await _allocationRepository.CreateAsync(leaveAllocation);
        
        return leaveAllocation.Id;
    }
}
