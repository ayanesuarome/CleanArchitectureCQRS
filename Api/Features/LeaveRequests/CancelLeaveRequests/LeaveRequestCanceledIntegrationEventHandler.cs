using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;
using CleanArch.Contracts.Identity;
using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Email;
using CleanArch.Infrastructure.Emails.Options;
using CleanArch.Api.Contracts.Emails;
using Microsoft.Extensions.Options;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public sealed class LeaveRequestCanceledIntegrationEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<EmailTemplateIdOptions> emailTemplateOptions) : IIntegrationEventHandler<LeaveRequestCanceledIntegrationEvent>
{
    public async Task Handle(LeaveRequestCanceledIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        IEmailSender emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

        Employee employee = await userService.GetEmployee(integrationEvent.EmployeeId);

        EmailMessageTemplate email = new()
        {
            To = employee.Email,
            TemplateId = emailTemplateOptions.Value.LeaveRequestCancelation,
            TemplateData = new EmailMessageDto(
                RecipientName: employee.FullName,
                Start: integrationEvent.StartDate,
                End: integrationEvent.EndDate,
                Now: integrationEvent.OcurredOn)
        };

        await emailSender.SendEmail(email);
    }
}
