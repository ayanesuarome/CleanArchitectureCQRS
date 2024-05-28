using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;
using CleanArch.Contracts.Identity;
using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Email;
using CleanArch.Infrastructure.Emails.Options;
using CleanArch.Api.Contracts.Emails;
using Microsoft.Extensions.Options;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public class LeaveRequestApprovalUpdatedIntegrationEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<EmailTemplateIdOptions> emailTemplateOptions) : IIntegrationEventHandler<LeaveRequestApprovalUpdatedIntegrationEvent>
{
    public async Task Handle(LeaveRequestApprovalUpdatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        IEmailSender emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

        Employee employee = await userService.GetEmployee(integrationEvent.EmployeeId);

        EmailMessageTemplate email = new()
        {
            To = employee.Email,
            TemplateId = emailTemplateOptions.Value.LeaveRequestApproval,
            TemplateData = new EmailMessageDto(
                RecipientName: employee.FullName,
                Start: integrationEvent.StartDate,
                End: integrationEvent.EndDate,
                Now: integrationEvent.OcurredOn)
            {
                IsApproved = integrationEvent.IsApproved
            }
        };

        await emailSender.SendEmail(email);
    }
}
