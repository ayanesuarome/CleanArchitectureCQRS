using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

    public CreateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IEmailSender emailSender,
        IAppLogger<CreateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        CreateLeaveRequestCommandValidator validator = new(_leaveTypeRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }

        LeaveRequest leaveAllocation = _mapper.Map<LeaveRequest>(request);
        await _leaveRequestRepository.CreateAsync(leaveAllocation);

        try
        {
            // send confirmation email
            EmailMessage email = new()
            {
                To = string.Empty, // TODO: get email from employee record
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                        $"has been created successfully.",
                Subject = "Leave Request Created"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return leaveAllocation.Id;
    }
}
