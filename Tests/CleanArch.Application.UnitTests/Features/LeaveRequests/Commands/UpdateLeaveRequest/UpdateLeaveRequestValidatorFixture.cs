using CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

namespace CleanArch.Application.UnitTests.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestValidatorFixture : IDisposable
{
    public UpdateLeaveRequestCommandValidator validator;

    public UpdateLeaveRequestValidatorFixture()
    {
        validator = new UpdateLeaveRequestCommandValidator();
    }

    public void Dispose()
    {
        validator = null;
    }
}
