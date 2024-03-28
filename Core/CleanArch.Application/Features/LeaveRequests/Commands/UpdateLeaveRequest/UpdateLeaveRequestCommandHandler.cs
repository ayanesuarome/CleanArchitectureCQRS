using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Result<LeaveRequest>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _repository;
    private readonly IValidator<UpdateLeaveRequestCommand> _validator;

    public UpdateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveRequestRepository repository,
        IValidator<UpdateLeaveRequestCommand> validator)
    {
        _mapper = mapper;
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<LeaveRequest>> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetByIdAsync(request.Id);

        if(leaveRequest == null)
        {
            return new NotFoundResult<LeaveRequest>(LeaveRequestErrors.NotFound(request.Id));
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            return new FailureResult<LeaveRequest>(LeaveRequestErrors.InvalidLeaveRequest(validationResult.ToDictionary()));
        }

        _mapper.Map(request, leaveRequest);
        await _repository.UpdateAsync(leaveRequest);

        return new SuccessResult<LeaveRequest>(leaveRequest);
    }
}
