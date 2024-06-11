using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Commands.UpdateLeaveRequests;

public class UpdateLeaveRequestValidatorFixture : IDisposable
{
    public UpdateLeaveRequest.Validator validator;

    public UpdateLeaveRequestValidatorFixture()
    {
        validator = new UpdateLeaveRequest.Validator();
    }

    public void Dispose()
    {
        validator = null;
    }
}
