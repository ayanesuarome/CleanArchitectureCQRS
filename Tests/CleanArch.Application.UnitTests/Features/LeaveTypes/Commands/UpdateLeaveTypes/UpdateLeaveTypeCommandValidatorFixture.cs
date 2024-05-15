using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

namespace CleanArch.Application.Tests.Features.LeaveTypes.Commands.UpdateLeaveTypes;

public class UpdateLeaveTypeCommandValidatorFixture : IDisposable
{
    internal UpdateLeaveType.Validator validator;

    public UpdateLeaveTypeCommandValidatorFixture()
    {
        validator = new UpdateLeaveType.Validator();
    }

    public void Dispose()
    {
        validator = null;
    }
}
