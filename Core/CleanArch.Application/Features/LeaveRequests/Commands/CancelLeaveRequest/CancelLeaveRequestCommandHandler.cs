using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models.Emails;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IUserService _userService;
    private readonly IEmailSender _emailSender;
    private readonly EmailTemplateIds _emailTemplateSettings;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _logger;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository repository,
        IUserService userService,
        IEmailSender emailSender,
        IOptions<EmailTemplateIds> emailTemplateSettings,
        IAppLogger<CancelLeaveRequestCommandHandler> logger)
    {
        _repository = repository;
        _userService = userService;
        _emailSender = emailSender;
        _emailTemplateSettings = emailTemplateSettings.Value;
        _logger = logger;
    }

    public async Task Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        leaveRequest.IsCancelled = true;
        await _repository.UpdateAsync(leaveRequest);

        try
        {
            Employee employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);

            // send confirmation email
            EmailMessageTemplate email = new()
            {
                To = employee.Email,
                TemplateId = _emailTemplateSettings.LeaveRequestCancelation,
                TemplateData = new EmailMessageCancelDto
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
