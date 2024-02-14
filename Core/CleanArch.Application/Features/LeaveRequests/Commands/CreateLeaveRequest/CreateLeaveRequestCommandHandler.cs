using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Extensions;
using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models.Emails;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _allocationRepository;
    private readonly IUserService _userService;
    private readonly IEmailSender _emailSender;
    private readonly EmailTemplateIds _emailTemplateSettings;

    private readonly IValidator<CreateLeaveRequestCommand> _validator;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

    public CreateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository allocationRepository,
        IUserService userService,
        IEmailSender emailSender,
        IOptions<EmailTemplateIds> emailTemplateSettings,
        IValidator<CreateLeaveRequestCommand> validator,
        IAppLogger<CreateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _allocationRepository = allocationRepository;
        _userService = userService;
        _emailSender = emailSender;
        _emailTemplateSettings = emailTemplateSettings.Value;
        _validator = validator;
        _logger = logger;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }

        // check on employee's allocation
        // TODO: measure whether a get or a bool makes improvements
        LeaveAllocation leaveAllocation = await _allocationRepository.GetEmployeeAllocation(_userService.UserId, request.LeaveTypeId);

        // if allocations aren't enough, return validation error
        if(leaveAllocation == null)
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

        try
        {
            Employee employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);

            // send confirmation email
            EmailMessageTemplate email = new()
            {
                To = employee.Email,
                TemplateId = _emailTemplateSettings.LeaveRequestCreate,
                TemplateData = new EmailMessageCreateDto
                {
                    RecipientName = employee.GetName(),
                    Start = leaveRequest.StartDate,
                    End = leaveRequest.EndDate,
                    Now = DateTimeOffset.Now
                }
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
