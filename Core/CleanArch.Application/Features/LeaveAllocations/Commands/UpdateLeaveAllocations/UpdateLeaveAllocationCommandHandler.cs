using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocations
{
    public class UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository repository)
        : IRequestHandler<UpdateLeaveAllocationCommand>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveAllocationRepository _repository = repository;

        public async Task Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(request.Id);

            if(leaveAllocation == null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            UpdateLeaveAllocationCommandValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                throw new BadRequestException($"Invalid {nameof(LeaveAllocation)}", validationResult);
            }

            _mapper.Map(request, leaveAllocation);
            await _repository.UpdateAsync(leaveAllocation);
        }
    }
}
