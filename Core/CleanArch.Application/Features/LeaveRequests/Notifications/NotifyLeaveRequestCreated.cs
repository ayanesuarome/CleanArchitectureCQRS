using CleanArch.Application.Events;
using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models.Emails;
using CleanArch.Application.Models.Identity;
using MediatR;
using Microsoft.Extensions.Options;

namespace CleanArch.Application.Features.LeaveRequests.Notifications;

public class NotifyLeaveRequestCreated : INotificationHandler<LeaveRequestCreated>
{
    private readonly IUserService _userService;
    private readonly IEmailSender _emailSender;
    private readonly EmailTemplateIds _emailTemplateSettings;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

    public NotifyLeaveRequestCreated(
        IUserService userService,
        IEmailSender emailSender,
        IOptions<EmailTemplateIds> emailTemplateSettings,
        IAppLogger<CreateLeaveRequestCommandHandler> logger)
    {
        _userService = userService;
        _emailSender = emailSender;
        _emailTemplateSettings = emailTemplateSettings.Value;
        _logger = logger;
    }

    public async Task Handle(LeaveRequestCreated notification, CancellationToken cancellationToken)
    {
        try
        {
            Employee employee = await _userService.GetEmployee(notification.LeaveRequest.RequestingEmployeeId);

            // send confirmation email
            EmailMessageTemplate email = new()
            {
                To = employee.Email,
                TemplateId = _emailTemplateSettings.LeaveRequestCreate,
                TemplateData = new EmailMessageCreateDto
                {
                    RecipientName = employee.GetName(),
                    Start = notification.LeaveRequest.StartDate,
                    End = notification.LeaveRequest.EndDate,
                    Now = notification.ActionDate
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
