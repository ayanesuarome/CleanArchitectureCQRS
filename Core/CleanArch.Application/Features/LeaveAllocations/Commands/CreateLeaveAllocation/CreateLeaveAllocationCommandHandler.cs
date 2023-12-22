using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _allocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationCommandHandler(IMapper mapper,
        ILeaveAllocationRepository allocationRepository,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _allocationRepository = allocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        CreateLeaveAllocationCommandValidator validator = new(_leaveTypeRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveAllocation)}", validationResult);
        }

        LeaveAllocation leaveAllocation = _mapper.Map<LeaveAllocation>(request);
        await _allocationRepository.CreateAsync(leaveAllocation);
        
        return leaveAllocation.Id;
    }
}
