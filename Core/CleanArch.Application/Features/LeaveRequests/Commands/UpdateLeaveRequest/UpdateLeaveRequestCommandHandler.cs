using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
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

namespace CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _repository;
    private readonly IUserService _userService;
    private readonly IEmailSender _emailSender;
    private readonly EmailTemplateIds _emailTemplateSettings;
    private readonly IValidator<UpdateLeaveRequestCommand> _validator;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

    public UpdateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveRequestRepository repository,
        IUserService userService,
        IEmailSender emailSender,
        IOptions<EmailTemplateIds> emailTemplateSettings,
        IValidator<UpdateLeaveRequestCommand> validator,
        IAppLogger<UpdateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _userService = userService;
        _emailSender = emailSender;
        _emailTemplateSettings = emailTemplateSettings.Value;
        _validator = validator;
        _logger = logger;
    }

    public async Task Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetByIdAsync(request.Id);

        if(leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }

        _mapper.Map(request, leaveRequest);
        await _repository.UpdateAsync(leaveRequest);

        try
        {
            Employee employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);

            // send confirmation email
                //Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                //        $"has been updated successfully.",
                //Subject = "Leave Request Updated"

            EmailMessageTemplate email = new()
            {
                To = employee.Email,
                // TODO: create template
                TemplateId = _emailTemplateSettings.LeaveRequestUpdate,
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
    }
}
